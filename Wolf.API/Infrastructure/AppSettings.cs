using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.API.Infrastructure.Logging;

namespace Wolf.API.Infrastructure
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public IdentityServerAuthentication IdentityServerAuthentication { get; set; }
        public FileServerConfiguration FileServerConfiguration { get; set; }
        public SSOConfig SSOConfig { get; set; }
        public LoggingOptions Logging { get; set; }
    }
    public class SSOConfig
    {
        public bool Enable { get; set; }
    }
    public class FileServerConfiguration
    {
        public string SavePath { get; set; }
    }
    public class IdentityServerAuthentication
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenExpiration { get; set; }
        public double RefreshTokenExpiration { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
    public class LoggingOptions
    {
        public Dictionary<string, string> LogLevel { get; set; }

        public FileOptions File { get; set; }
    }
    public class FileOptions
    {
        public LogEventLevel MinimumLogEventLevel { get; set; }
    }
}
