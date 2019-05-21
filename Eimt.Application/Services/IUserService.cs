using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.Impl;
using Eimt.Application.Services.ResultModels;
using System.Threading.Tasks;

namespace Eimt.Application.Services
{
    public interface IUserService
    {
        Task RegisterNewUser(UserDto userDto);
        bool ConfirmUserToken(string email, string token);

        Task<ForgotPasswordResult> ForgotPassword(string Email);
        ChangePasswordResult ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
