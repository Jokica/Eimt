using Eimt.Domain;
using Eimt.Domain.DomainModels;

namespace Eimt.Application.Interfaces
{
    public interface IUserRepository:IRepository<User,long>
    {
        User FindUserByEmail(string email);
        User FindUserByEmailincludeToken(string email);
        User FindUserByEmailWithRoles(string email);
    }
}