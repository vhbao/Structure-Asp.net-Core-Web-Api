using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service
{
    public interface IServiceWrapper
    {
        Sys_AuthToken.IService Sys_AuthToken { get; }
        Sys_File.IService Sys_File { get; }
        Sys_User.IService Sys_User { get; }
        Sys_Category.IService Sys_Category { get; }
        Sys_Organization.IService Sys_Organization { get; }
        Sys_Role.IService Sys_Role { get; }
        Sys_Config.IService Sys_Config { get; }
        Sys_Permission.IService Sys_Permission { get; }
        Sys_Resource.IService Sys_Resource { get; }
    }
}
