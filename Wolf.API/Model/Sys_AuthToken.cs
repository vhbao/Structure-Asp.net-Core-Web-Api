using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Model
{
    [Table("Sys_Authtokens")]
    public class Sys_AuthToken : AuditEntity
    {
        [StringLength(55)]
        public string LoginName { get; set; }
        [StringLength(800)]
        public string AccessToken { get; set; }
        [StringLength(500)]
        public string RefeshToken { get; set; }
    }
}
