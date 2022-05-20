using Microsoft.EntityFrameworkCore;
using Wolf.API.Infrastructure;
using Wolf.Core.Constant;
using Wolf.Core.ExtensionMethods;
using Wolf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_Role
{
    public class Service:RepositoryBase<Model.Sys_Role>, Sys_Role.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService):base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string Code)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Code))
            {
                throw new Exception(Sys_Const.Message.SERVICE_CODE_NOT_EMPTY);
            }
            if (GuidExtensions.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Sys_Roles.Where(o => o.Code == Code).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Sys_Roles.Where(o => o.Id == Id && o.Code == Code).CountAsync();
                if (count <= 1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return await Task.FromResult(result);
        }
        public async Task DeleteById(Guid Id)
        {
            var user = await _dbContext.Sys_Users_Roles.FirstOrDefaultAsync(o => o.RoleId == Id);
            if (user != null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ROLE_EXIST_USER);
            }
            var role = await _dbContext.Sys_Roles.FirstOrDefaultAsync(o => o.Id == Id);
            if(role == null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ROLE_UNEXISTED);
            }    
            _dbContext.Sys_Roles.Remove(role);
            await UnitOfWork.SaveAsync();
        }
    }
}
