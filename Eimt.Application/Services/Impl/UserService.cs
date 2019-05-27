using Eimt.Application.Extensions;
using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.ResultModels;
using Eimt.Application.Services.ViewModels;
using Eimt.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eimt.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserMessageSender messageSender;
        private readonly IRoleService roleService;

        public UserService(IUnitOfWork unitOfWork, IUserMessageSender messageSender, IRoleService roleService)
        {
            repository = unitOfWork.UserRepository;
            this.unitOfWork = unitOfWork;
            this.messageSender = messageSender;
            this.roleService = roleService;
        }
        public async Task<ForgotPasswordResult> ForgotPassword(string email)
        {
            User user = repository.FindUserByEmail(email);
            if (user == null)
                return new ForgotPasswordResult { Message = "Email Doesn't Exist", Success = false };
            var generatedPassword = user.ResetPassword();
            await messageSender.SendResetPasswordEmail(email, generatedPassword);
            unitOfWork.Commit();
            return new ForgotPasswordResult
            {
                Message = "Your password was send to you.Check your Email",
                Success = true
            };
        }
        public async Task RegisterNewUser(RegisterUserDto userDto)
        {
            var user = new User(userDto.Email, userDto.Password);
            repository.Add(user);
            unitOfWork.Commit();
            await messageSender.SendConfirmationToken(userDto.Email, user.Token.SecurityStamp);
        }
        public ChangePasswordResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = repository.FindUserByEmail(changePasswordDto.Email);
            if (user == null)
            {
                return new ChangePasswordResult { Message = "User Doesn't exists" };
            }
            if (!user.IsEmailConfirmed)
            {
                return new ChangePasswordResult { Message = "User account is not confirmed" };
            }
            var result = user.ChangePassword(changePasswordDto.OldPassword, changePasswordDto.Password);
            if (result.Success)
            {
                unitOfWork.Commit();
            }
            return new ChangePasswordResult(result);
        }
        public bool ConfirmUserToken(string email, string token)
        {
            var user = repository.FindUserByEmailincludeToken(email);
            if (user == null)
            {
                return false;
            }
            if (user.IsEmailConfirmed)
            {
                return true;
            }
            var res = user.ConfirmUser(token);
            roleService.AddUserDefualtRole(user);
            if (res)
                unitOfWork.Commit();
            return res;
        }

        public UserDto GetUser(long id)
        {
            return repository.FindFull(id).ToDto();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return repository
                .FindAllUsersWithRoles()
                .Select(x => x.ToDto());
        }

        public IEnumerable<UserDto> GetUsersBySector(string sector)
        {
            return repository
                   .FindSectorUsersWithRoles(sector)
                   .Select(x => x.ToDto());
        }

        public void DeleteUser(long id)
        {
            var user = repository.Find(id);
            repository.Remove(user);
            unitOfWork.Commit();
        }
    }
}
