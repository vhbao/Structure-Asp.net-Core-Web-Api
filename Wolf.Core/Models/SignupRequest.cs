using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Models
{
    public class SignupRequest
    {
        [Required]
        [StringLength(55)]
        public string FullName { get; set; }
        [Required]
        [StringLength(55)]
        public string LoginName { get; set; }
        [Required]
        [StringLength(55)]
        public string PassWord { get; set; }
        [StringLength(55)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Address { get; set; }        
    }
}
