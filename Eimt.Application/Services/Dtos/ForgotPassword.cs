using System.ComponentModel.DataAnnotations;

namespace Eimt.Application.Services.Dtos
{
    public class ForgotPassword
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
