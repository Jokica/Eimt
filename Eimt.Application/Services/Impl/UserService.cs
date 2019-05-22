using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Dtos;
using Eimt.Application.Services.Dtos;
using Eimt.Application.Services.ResultModels;
using Eimt.Domain;
using Eimt.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eimt.Application.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMessageSender messageSender;
        private readonly IRoleService roleService;

        public UserService(IUnitOfWork unitOfWork,IMessageSender messageSender,IRoleService roleService)
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
               return new ForgotPasswordResult { Message = "Email Doesn't Exist" ,Success = false};
            var generatedPassword = user.ResetPassword();
            await messageSender.SendResetPasswordEmail(email, generatedPassword);
            return new ForgotPasswordResult {
                Message = "Your password was send to you.Check your Email",
                Success = true
            };
        }
        public async Task RegisterNewUser(UserDto userDto)
        {
            using (var transaction = unitOfWork.CreateTransaction())
            {
                var user = new User(userDto.Email, userDto.Password);
                repository.Add(user);
                unitOfWork.Commit();
                await messageSender.SendConfirmationToken(userDto.Email, user.Token.SecurityStamp);
                transaction.Commit();
            }
        }
        public ChangePasswordResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = repository.FindUserByEmail(changePasswordDto.Email);
            if (user == null)
            {
                return new ChangePasswordResult {Message = "User Doesn't exists" };
            }
            if (!user.IsEmailConfirmed)
            {
                return new ChangePasswordResult { Message = "User account is not confirmed" };
            }
            var result = user.ChangePassword(changePasswordDto.OldPassword,changePasswordDto.Password);
            if (result.Success)
            {
                unitOfWork.Commit();
            }
            return new ChangePasswordResult(result);
        }
        public bool ConfirmUserToken(string email,string token)
        {
            var user = repository.FindUserByEmailincludeToken(email);
            if(user == null)
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
    }
}
