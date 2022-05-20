using Microsoft.EntityFrameworkCore;
using Wolf.API.Infrastructure;
using Wolf.Core.Constant;
using Wolf.Core.ExtensionMethods;
using Wolf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_AuthToken
{
    public class Service : RepositoryBase<Model.Sys_AuthToken>, Sys_AuthToken.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) : base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task SaveByLoginNameAsync(string loginName, Model.Sys_AuthToken authToken)
        {
            var token = _dbContext.Sys_AuthTokens.Where(o => o.LoginName == loginName).FirstOrDefault();
            if (token == null)
            {
                authToken.LoginName = loginName;
                authToken.CreatedBy = loginName;
                authToken.CreatedDateTime = _dateTimeProvider.OffsetNow;
                await _dbContext.Sys_AuthTokens.AddAsync(authToken);
            }
            else
            {
                ObjectExtensions.Mapping<Model.Sys_AuthToken, Model.Sys_AuthToken>(authToken, token, new string[] { "id" });
                token.LoginName = loginName;
                token.UpdatedBy = loginName;
                token.UpdatedDateTime = _dateTimeProvider.OffsetNow;
                //_dbContext.Sys_AuthTokens.Update(token);
                _dbContext.Entry(token).CurrentValues.SetValues(token);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Model.Sys_AuthToken> GetByLogiNameAsync(string loginName)
        {
            return await _dbContext.Sys_AuthTokens.Where(o => o.LoginName == loginName).FirstOrDefaultAsync();
        }
    }
}
