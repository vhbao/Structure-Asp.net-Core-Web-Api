using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_AuthToken
{
    public interface IService: IRepositoryBase<Model.Sys_AuthToken>
    {
        Task SaveByLoginNameAsync(string loginName, Model.Sys_AuthToken authToken);
        Task<Model.Sys_AuthToken> GetByLogiNameAsync(string loginName);
    }
}
