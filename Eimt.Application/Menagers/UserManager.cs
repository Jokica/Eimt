using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eimt.Application.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpContext context;
        private readonly IUserRepository repository;
        public UserManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor.HttpContext;
            repository = unitOfWork.UserRepository;
        }
        public async Task<LoginResult> Login(string email, string password, bool remamberMe)
        {

            User user = repository.FindUserByEmailWithRoles(email);
            if (user == null || !user.IsEmailConfirmed || !user.DoesPasswordMatch(password))
            {
                return new LoginResult
                {
                    Message = "Unable to login!",
                    Success = false
                };
            }
            List<Claim> claims = user
                                    .Roles
                                    .Select(x => new Claim(ClaimTypes.Role, x.Role.Name))
                                    .ToList();
            if (claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Menager"))
            {
                claims.Add(new Claim("Sector", user.ManagesSector.Name));
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Email));


            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProporties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(2),
                IsPersistent = remamberMe,
                IssuedUtc = DateTime.Now,
            };
            await context.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(ClaimsIdentity),
                  authProporties);
            return new LoginResult
            {
                Success = true
            };
        }

        public async Task LogOut() => await context.SignOutAsync();
    }
}
