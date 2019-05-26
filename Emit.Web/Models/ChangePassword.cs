using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emit.Web.Models
{
    public class ChangePassword
    {
        [DataType(DataType.Password)]
        [Required]
        [Display(Name="Old Password")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
