using Wolf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_Category
{
    public interface IService: IRepositoryBase<Model.Sys_Category>
    {
        Task<bool> IsDupicateAttributesAsync(Guid? Id, string Code, int Type);
    }
}
