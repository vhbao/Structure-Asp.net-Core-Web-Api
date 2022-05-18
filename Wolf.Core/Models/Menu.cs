using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Models
{
    public class Menu
    {
        public string Name { get; set; }
        public Meta Meta { get; set; }
        public Menu[] Children { get; set; }
    }
    public class Meta
    {
        public string Title { get; set; }
    }
}
