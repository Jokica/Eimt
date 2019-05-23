using System.ComponentModel.DataAnnotations;

namespace Eimt.Application.Interfaces.Dtos
{
    public class RegisterUserDto
    {
       
        [Compare("Password",ErrorMessage ="Passwords must match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get;  set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get;  set; }
    }
}
