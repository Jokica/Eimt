using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.Impl;
using Eimt.Application.Services.ResultModels;
using Eimt.Application.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eimt.Application.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        Task RegisterNewUser(RegisterUserDto userDto);
        bool ConfirmUserToken(string email, string token);

        Task<ForgotPasswordResult> ForgotPassword(string Email);
        ChangePasswordResult ChangePassword(ChangePasswordDto changePasswordDto);
        IEnumerable<UserDto> GetUsersBySector(string sector);
    }
}
