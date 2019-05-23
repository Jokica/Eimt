using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eimt.Application.Services.ViewModels
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Sector { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        [Display(Name ="User Confirmed")]
        public bool IsConfirmed { get; set; }
    }
}
