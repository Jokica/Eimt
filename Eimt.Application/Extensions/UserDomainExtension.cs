using Eimt.Application.Services.ViewModels;
using Eimt.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eimt.Application.Extensions
{
    public static class UserDomainExtension
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                IsConfirmed = user.IsEmailConfirmed,
                Roles = user.Roles?.Select(x => x.Role?.Name).ToList() ?? new List<string>()
            };
        }
    }
}
