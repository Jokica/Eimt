using System.Collections.Generic;
using Eimt.Application.Services.ViewModels;
using Eimt.Domain;
using Eimt.Domain.DomainModels;

namespace Eimt.Application.Interfaces
{
    public interface IUserRepository:IRepository<User,long>
    {
        User FindUserByEmail(string email);
        User FindUserByEmailincludeToken(string email);
        User FindUserByEmailWithRoles(string email);
        IEnumerable<User> FindAllUsersWithRoles();
        IEnumerable<User> FindSectorUsersWithRoles(string sector);

    }
}
