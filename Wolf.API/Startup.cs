using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Wolf.API.Infrastructure.Authorization;
using Wolf.API.Service;
using Wolf.API.Infrastructure;
using Wolf.API.Infrastructure.Authentication;
using Wolf.Core.Core;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Wolf.API.Infrastructure.Middleware;
using Wolf.API.Infrastructure.BackgroundTasks;

namespace Wolf.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = new AppSettings();
            Configuration.Bind(AppSettings);
        }

        public IConfiguration Configuration { get; }
        private AppSettings AppSettings { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddControllers();
            ConfigureHealthCheckService(services);
            ConfigureSwaggerService(services);

            services.AddDbContextPool<DomainDbContext>(options => options.UseSqlServer(AppSettings.ConnectionString), poolSize: 128);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<Core.Interfaces.IUploadFileProvider, UploadFileProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped(typeof(IServiceWrapper), typeof(ServiceWrapper));
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            ConfigureAuthService(services);
            services.AddSingleton<GlobalVariable>();
            services.AddHostedService<TimedHostedService>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureSwagger(app);
            app.UseRouting();
            app.UseCors("AllowAnyOrigin");
            ConfigureFileServer(app);
            ConfigureAuth(app);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();//healthchecks-ui
            });

            app.UseHomeMiddleware();
        }

        #region Auth
        private void ConfigureAuthService(IServiceCollection services)
        {
            var jwtTokenConfig = Configuration.GetSection("IdentityServerAuthentication").Get<IdentityServerAuthentication>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                //x.Authority = AppSettings.IdentityServerAuthentication.Authority;
                //x.Audience = AppSettings.IdentityServerAuthentication.ApiName;
                x.RequireHttpsMetadata = AppSettings.IdentityServerAuthentication.RequireHttpsMetadata;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AppSettings.IdentityServerAuthentication.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.IdentityServerAuthentication.Secret)),
                    ValidAudience = AppSettings.IdentityServerAuthentication.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
        }
        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
        #endregion
        #region Swagger
        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wolf", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
                c.CustomSchemaIds(i => i.FullName);
            });
        }
        protected virtual void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();//swagger
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wolf v1"));
        }
        #endregion
        #region HealthCheck
        private void ConfigureHealthCheckService(IServiceCollection services)
        {
            services.AddHealthChecks()
           .AddSqlServer(AppSettings.ConnectionString,
           healthQuery: "select 1",
           failureStatus: HealthStatus.Degraded,
           name: "SQL Server")
           .AddCheck<TodoHealthCheck>("Todo Health Check", failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("default api", "/api/health"); //map health check api    
            })
            .AddInMemoryStorage();
        }
        #endregion
        #region FileServer
        protected virtual void ConfigureFileServer(IApplicationBuilder app)
        {
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                EnableDefaultFiles = true
            });
        }
        #endregion
    }
}