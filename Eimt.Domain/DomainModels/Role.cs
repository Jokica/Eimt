using System.Collections.Generic;

namespace Eimt.Domain.DomainModels
{
    public class Role
    {
        public List<UserRoles> Users { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }

        public Role(string name)
        {
            Name = name;
            Users = new List<UserRoles>();
        }
        private Role()
        {

        }
        public void AddUser(User user)
        {
            Users.Add(new UserRoles(this, user));
        }
    }
}
