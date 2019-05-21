using Eimt.Domain.DomainDTOs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Eimt.Domain.DomainModels
{
    public class User
    {
        public long Id { get;  set; }
        public bool IsEmailConfirmed { get; set; }
        public int AccessCount { get; private set; }
        public UserConfirmationToken Token { get; set; }
        public string Email { get;  set; }
        public List<UserRoles> Roles { get; set; }
        public string Password { get; private set; }

        public User(string email, string password)
        {
            Email = email;
            SetPassword(password);
            AccessCount = 0;
            Token = new UserConfirmationToken(this, new Guid().ToString().Substring(0, 8));
        }
        private User()
        {

        }
        private void SetPassword(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            using (var sha = SHA256.Create())
            {
                data = sha.ComputeHash(data, 0, data.Length);
                Password = BitConverter.ToString(data).Replace("-", "");
            }
        }
        public string ResetPassword()
        {
            var generatedPassword = new Guid().ToString().Substring(0, 8);
            SetPassword(generatedPassword);
            return generatedPassword;
        }
        public bool DoesPasswordMatch(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            using (var sHA = SHA256.Create())
            {
                data = sHA.ComputeHash(data,0,data.Length);
                return Password == BitConverter.ToString(data).Replace("-", "");
            }
        }
        public ChangePasswordResult ChangePassword(string oldPasword,string newPassword)
        {
            if (!DoesPasswordMatch(oldPasword))
                return new ChangePasswordResult { Message = "Old password doesn't match current password" };
            SetPassword(newPassword);
            return new ChangePasswordResult();
        }

        public bool ConfirmUser(string token)
        {
            if(Token != null && token == Token.SecurityStamp)
            {
                IsEmailConfirmed = true;
                this.Token = null;
            }
            return IsEmailConfirmed;
        }
    }

}
