using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_Resource
{
    public interface IService: IRepositoryBase<Model.Sys_Resource>
    {
        Task<List<Model.Sys_Resource>> InitFunctionAsync();
        Task<List<Model.Sys_Resource>> InitMenuAsync(List<Menu> menu);
        Task<List<ViewModel.Sys_Resource.ResourceTree>> GetFunctionTreeAsync();
        Task<List<ViewModel.Sys_Resource.ResourceTree>> GetMenuTreeAsync();
        Task DeleteAllMenu();
        Task DeleteAllFunction();
    }
}
