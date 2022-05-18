using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Models
{
    public class LoginResult
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }                
    }
}
