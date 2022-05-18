using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_Organization
{
    public interface IService: IRepositoryBase<Model.Sys_Organization>
    {
        Task<List<ViewModel.Sys_Organization.OrganTree>> GetTreeAsync();
        Task<List<Model.Sys_Organization>> GetByParentIdAsync(Guid ParentId);
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Code);
        Task DeleteById(Guid Id);
    }
}
