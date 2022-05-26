using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Wolf.API.Infrastructure;
using Wolf.API.Model;
using Wolf.Core.Constant;
using Wolf.Core.Core;
using Wolf.Core.Helpers;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_User
{
    public class Service:RepositoryBase<Model.Sys_User>, Sys_User.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;                
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService) :base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;            
        }
        public async Task<bool> IsDupicateAttributesAsync(Guid? Id, string LoginName)
        {
            bool result = false;
            if (string.IsNullOrEmpty(LoginName))
            {
                throw new Exception(Sys_Const.Message.SERVICE_CODE_NOT_EMPTY);
            }
            if (GuidHelpers.IsNullOrEmpty(Id))
            {
                result = await _dbContext.Sys_Users.Where(o => o.LoginName == LoginName).AnyAsync();
            }
            else
            {
                var count = await _dbContext.Sys_Users.Where(o => o.Id == Id && o.LoginName == LoginName).CountAsync();
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
        private async Task ValidateUserAsync(Guid OrganId, Guid RoleId)
        {
            var role = await _dbContext.Sys_Roles.AnyAsync(o => o.Id == RoleId);
            if (!role)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ROLE_UNEXISTED);
            }
            var organ = await _dbContext.Sys_Organizations.AnyAsync(o => o.Id == OrganId);
            if (!organ)
            {
                throw new Exception(Sys_Const.Message.SERVICE_ORGAN_UNEXISTED);
            }
        }
        public async Task DeleteById(Guid Id)
        {            
            _dbContext.Sys_Users_Roles.RemoveRange(_dbContext.Sys_Users_Roles.Where(o => o.UserId == Id).ToList());
            var user = await _dbContext.Sys_Users.FirstOrDefaultAsync(o => o.Id == Id);
            if (user != null)
            {
                throw new Exception(Sys_Const.Message.SERVICE_USERNAME_UNEXISTED);
            }
            _dbContext.Sys_Users.Remove(user);
            await UnitOfWork.SaveAsync();
        }
        public async Task<ViewModel.Sys_User.Detail> GetDetailByIdAsync(Guid id)
        {
            var query = (from ur in _dbContext.Sys_Users_Roles
                         join u in _dbContext.Sys_Users on ur.UserId equals u.Id
                         where u.Id == id && ur.IsDefault == true
                         select new ViewModel.Sys_User.Detail()
                         {
                             Id = u.Id,
                             FullName = u.FullName,
                             LoginName = u.LoginName,
                             Email = u.Email,
                             Phone = u.Phone,
                             Address = u.Address,
                             IsActive = u.IsActive,
                             RoleId = ur.RoleId,
                             OrganId = ur.OrganId,
                         });
            return await query.FirstOrDefaultAsync();
        }
        public async Task<ViewModel.Sys_User.Detail> CreateAsync(Model.Sys_User user, Guid organId, Guid roleId)
        {
            try
            {
                UnitOfWork.CreateTransaction();
                await ValidateUserAsync(organId, roleId);
                //add user                
                user.PassWord = Cryption.EncryptByKey(user.PassWord, Sys_Const.Security.key);
                user.IsSystem = false;
                await AddOrUpdateAsync(user);
                //add user and role
                Sys_User_Role userRoles = new Sys_User_Role();
                userRoles.UserId = user.Id;
                userRoles.OrganId = organId;
                userRoles.RoleId = roleId;
                userRoles.IsDefault = true;
                await _dbContext.Sys_Users_Roles.AddAsync(userRoles);
                //mapping
                ViewModel.Sys_User.Detail userDetail = new ViewModel.Sys_User.Detail();
                ObjectHelpers.Mapping<Model.Sys_User, ViewModel.Sys_User.Detail>(user, userDetail);
                userDetail.OrganId = userRoles.OrganId;
                userDetail.RoleId = userRoles.RoleId;
                await UnitOfWork.SaveAsync();
                UnitOfWork.Commit();
                return userDetail;
            }
            catch (Exception ex)
            {
                UnitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }
        public async Task<ViewModel.Sys_User.Detail> UpdateAsync(Model.Sys_User user, Guid organId, Guid roleId)
        {
            try
            {
                UnitOfWork.CreateTransaction();
                await ValidateUserAsync(organId, roleId);
                //add user                    
                if (!string.IsNullOrEmpty(user.PassWord))
                {
                    user.PassWord = Cryption.EncryptByKey(user.PassWord, Sys_Const.Security.key);
                }
                user.IsSystem = true;
                await AddOrUpdateAsync(user);
                //add user and role
                Sys_User_Role userRoles = await _dbContext.Sys_Users_Roles.FirstOrDefaultAsync(o => o.UserId == user.Id && o.IsDefault == true);
                userRoles.OrganId = organId;
                userRoles.RoleId = roleId;
                //_dbContext.Sys_Users_Roles.Update(userRoles);
                _dbContext.Entry(userRoles).CurrentValues.SetValues(userRoles);
                //mapping
                ViewModel.Sys_User.Detail userDetail = new ViewModel.Sys_User.Detail();
                ObjectHelpers.Mapping<Model.Sys_User, ViewModel.Sys_User.Detail>(user, userDetail);
                userDetail.OrganId = userRoles.OrganId;
                userDetail.RoleId = userRoles.RoleId;
                await UnitOfWork.SaveAsync();
                UnitOfWork.Commit();
                return userDetail;
            }
            catch (Exception ex)
            {
                UnitOfWork.Roolback();
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<ViewModel.Sys_User.ListByOrganId>> GetByOrganIdAsync(Guid OrganId)
        {
            var users = await (from ur in _dbContext.Sys_Users_Roles
                         join u in _dbContext.Sys_Users on ur.UserId equals u.Id
                         join r in _dbContext.Sys_Roles on ur.RoleId equals r.Id
                         where ur.OrganId == OrganId && ur.IsDefault == true
                         select new ViewModel.Sys_User.ListByOrganId()
                         {
                             Id = u.Id,
                             RoleName = r.Name,
                             FullName = u.FullName,
                             LoginName = u.LoginName,
                             Email = u.Email,
                             Phone = u.Phone,
                             IsActive = u.IsActive
                         }).ToListAsync();
            return users;
        }
        public async Task<LoginResult> CheckUserLogin(string LoginName, string Password)
        {
            var obj = await (from a in _dbContext.Sys_Users
                             where a.LoginName == LoginName
                             select new
                             {
                                 UserId = a.Id,
                                 LoginName = a.LoginName,
                                 Password = a.PassWord,
                                 IsActive = a.IsActive
                             }).FirstOrDefaultAsync();
            if (obj == null)
            {
                throw new(Sys_Const.Message.SERVICE_PASS_INCORRECT);
            }
            else
            {
                if (Cryption.DecryptByKey(obj.Password, Sys_Const.Security.key) != Password)
                {
                    throw new(Sys_Const.Message.SERVICE_PASS_INCORRECT);
                }
                if (!obj.IsActive)
                {
                    throw new(Sys_Const.Message.SERVICE_USERNAME_UNACTIVE);
                }
            }
            return new LoginResult() { UserId = obj.UserId, UserName = obj.LoginName };
        }
        public async Task<LoginResult> CheckUserExisted(string LoginName)
        {
            var userExisted = await (from a in _dbContext.Sys_Users
                             where a.LoginName == LoginName
                                     select new LoginResult()
                             {
                                 UserId = a.Id,
                                 UserName = a.LoginName
                             }).FirstOrDefaultAsync();
            return userExisted;
        }
        public async Task UserChangePassword(Guid UserId, string PassWord)
        {
            var oUser = _dbContext.Sys_Users.Single(o => o.Id == UserId);
            if (!string.IsNullOrEmpty(PassWord))
            {
                oUser.PassWord = Cryption.EncryptByKey(PassWord, Sys_Const.Security.key);
            }
            //_dbContext.Sys_Users.Update(oUser);
            _dbContext.Entry(oUser).CurrentValues.SetValues(oUser);
            await UnitOfWork.SaveAsync();
        }
        public async Task<UserInfo> GetUserInfo(string UserName)
        {
            UserInfo userInfo = new UserInfo();   
            var userCurrent = await _dbContext.Sys_Users.FirstOrDefaultAsync(o => o.LoginName == UserName);
            ObjectHelpers.Mapping<Model.Sys_User, UserInfo>(userCurrent, userInfo);
            if (userCurrent != null)
            {
                userInfo.Roles = new List<string>();
                var roles = await (from x in _dbContext.Sys_Users_Roles
                                   join y in _dbContext.Sys_Roles on x.RoleId equals y.Id
                                   where x.UserId == userCurrent.Id
                                   select y.Code).ToListAsync();
                if (roles.Count != 0)
                {
                    userInfo.Roles.AddRange(roles);
                }
                if (userCurrent.LoginName == "admin" && userCurrent.IsSystem)
                {
                    userInfo.Roles.Add("admin");
                }
            }    
            return userInfo;
        }
        public async Task<LoginResult> CheckUserRefreshToken(string LoginName)
        {
            var query = from x in _dbContext.Sys_Users
                        where x.LoginName == LoginName
                        select new LoginResult
                        {
                            UserId = x.Id,
                            UserName = x.LoginName
                        };
            return await query.FirstOrDefaultAsync();
        }
    }
}
