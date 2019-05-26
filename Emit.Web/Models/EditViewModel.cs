using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Models
{
    public class EditViewModel
    {
        public List<string> Roles { get; set; }
        public List<string> UserRoles { get; set; }
        public List<string> Sectors { get; set; }
        public long Id { get; set; }
    }
}
