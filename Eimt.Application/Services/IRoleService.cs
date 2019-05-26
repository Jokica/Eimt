using Eimt.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eimt.Application.Services
{
    public interface IRoleService
    {
        Role FindOrCreateDefualtRole();
        void AddUserDefualtRole(User user,bool saveChanges = false);
        IEnumerable<string> GetUserRoles(long id);
        IEnumerable<string> GetRoles();
    }
}
