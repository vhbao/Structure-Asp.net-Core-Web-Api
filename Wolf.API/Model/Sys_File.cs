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
    [Table("Sys_Files")]
    public class Sys_File : AuditEntity
    {
        [StringLength(100)]
        public string Name { get; set; } = "";
        [StringLength(10)]
        public string Extension { get; set; } = "";
        [StringLength(100)]
        public string Path { get; set; } = "";
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
    }
}
