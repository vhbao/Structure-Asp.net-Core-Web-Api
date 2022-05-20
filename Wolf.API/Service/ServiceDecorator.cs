using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Wolf.API.Service
{
    class ServiceDecorator<TEntity>
    {
        private IRepositoryBase<TEntity> _serviceBase;
        public ServiceDecorator(IServiceWrapper service)
        {
            #region add service
            if (typeof(TEntity) == typeof(Model.Sys_Category))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Category;
            }
            else if(typeof(TEntity) == typeof(Model.Sys_User))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_User;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_File))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_File;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Organization))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Organization;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Role))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Role;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Config))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Config;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Permission))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Permission;
            }
            else if (typeof(TEntity) == typeof(Model.Sys_Resource))
            {
                _serviceBase = (IRepositoryBase<TEntity>)service.Sys_Resource;
            }
            #endregion
        }
        public async Task<TEntity> SaveEntityAsync(TEntity entity)
        {
            return await _serviceBase.SaveEntityAsync(entity);
        }
        public async Task<Paged<TEntity>> GetPagedAsync(int page, int pageSize, int totalLimitItems, string search)
        {
            return await _serviceBase.GetPagedAsync(page, pageSize, totalLimitItems, search);
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _serviceBase.GetByIdAsync(id);
        }
        public async Task Delete(List<TEntity> entity)
        {
            await _serviceBase.DeleteSave(entity);
        }
    }
}
