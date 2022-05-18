using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_User
{
    public interface IService: IRepositoryBase<Model.Sys_User>
    {        
        Task<LoginResult> CheckUserLogin(string UserName, string Password);        
        Task<LoginResult> CheckUserExisted(string UserName);
        Task UserChangePassword(Guid UserId, string PassWord);
        Task<LoginResult> CheckUserRefreshToken(string UserName);
        Task<UserInfo> GetUserInfo(string UserName);
        Task<List<ViewModel.Sys_User.ListByOrganId>> GetByOrganIdAsync(Guid OrganId);
        Task<ViewModel.Sys_User.Detail> GetDetailByIdAsync(Guid Id);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string LoginName);
        Task<ViewModel.Sys_User.Detail> CreateAsync(Model.Sys_User user, Guid OrganId, Guid RoleId);
        Task<ViewModel.Sys_User.Detail> UpdateAsync(Model.Sys_User user, Guid OrganId, Guid RoleId);
        Task DeleteById(Guid Id);
    }
}
