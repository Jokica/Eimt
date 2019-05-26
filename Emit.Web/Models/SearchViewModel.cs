using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Models
{
    public enum Roles
    {
        User = 0,
        Menager=1,
        Admin = 2,

    }
    public class SearchViewModel
    {
    
        public string Search { get; set; }
        public List<string> SearchableColumns { get; set; }
        public string SearchBy { get; set; } 
        public Roles Role { get; set; }
    }
}
