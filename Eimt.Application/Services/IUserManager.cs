using Eimt.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eimt.Application.Interfaces
{
    public interface IUserManager
    {
         Task<LoginResult> Login(string email, string password, bool remamberMe);
         Task LogOut();
    }
}
