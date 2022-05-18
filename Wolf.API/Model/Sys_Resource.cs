using Wolf.Core.Enums;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Model
{
    [Table("Sys_Resources")]
    public class Sys_Resource : AuditEntity
    {
        [StringLength(55)]
        public string Code { get; set; }
        [StringLength(55)]
        public string Name { get; set; }                
        public ResourceType Type { get; set; }
        public Guid ParentId { get; set; } = Guid.Empty;
    }
}
