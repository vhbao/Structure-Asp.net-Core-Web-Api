using Wolf.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.ViewModel.Sys_User
{
    public class ListByOrganId
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }        
        public string LoginName { get; set; }
        public string Email { get; set; }        
        public string Phone { get; set; }                
        public bool IsActive { get; set; } 
    }
}
