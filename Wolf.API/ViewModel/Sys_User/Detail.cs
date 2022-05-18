using Wolf.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.ViewModel.Sys_User
{
    public class Detail
    {
        public Guid Id { get; set; }
        public Guid OrganId { get; set; } 
        public Guid RoleId { get; set; }
        [StringLength(55)]
        public string FullName { get; set; }
        [StringLength(55)]
        public string LoginName { get; set; }
        [StringLength(55)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
