using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Enumeration
{
    public static class Sys_Enum
    {
        public enum StatusCode
        {
            Success = 200,
            UnAuthorize = 401,
            NotFound = 404,
            InternalError = 500,
            TokenExpired = 600
        }
        public enum AuditType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }
    }
}
