using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace Eimt.Application.Services.Impl
{
    public class RoleService : IRoleService
    {
        private string _defualtRole = "User";
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository repository;
        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.RoleRepository;
        }
        public Role FindOrCreateDefualtRole()
        {
            var role = repository.FirstOrDefualt(x => x.Name == _defualtRole);
            if (role == null)
            {
                role = new Role(_defualtRole);
                repository.Add(role);
                unitOfWork.Commit();
            }
            return role;
        }
        public void AddUserDefualtRole(User user, bool saveChanges = false)
        {
            var role = FindOrCreateDefualtRole();
            role.Users.Add(new UserRoles(role, user));
            if (saveChanges)
                unitOfWork.Commit();
        }

        public IEnumerable<string> GetUserRoles(long id)
        {
            return unitOfWork.UserRepository
                  .FindWithRoles(id)
                  .Roles
                  .Select(x => x.Role.Name);
        }

        public IEnumerable<string> GetRoles()
        {
            return unitOfWork.RoleRepository.GetAll().Select(x => x.Name);
        }
    }
}
