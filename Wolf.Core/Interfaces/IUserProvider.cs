using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Interfaces
{
    public interface IUserProvider
    {
        bool IsAuthenticated { get; }        
        Guid Id { get; }
        string LoginName { get; }
    }
}
