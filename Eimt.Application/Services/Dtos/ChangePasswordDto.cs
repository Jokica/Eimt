namespace Eimt.Application.Services.Dtos
{
    public class ChangePasswordDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
    }
}