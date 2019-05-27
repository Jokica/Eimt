namespace Eimt.Domain.DomainModels
{
    public class UserRoles
    {
        public User User { get; set; }
        public long UserId { get; set; }

        public Role Role { get; set; }
        public long RoleId { get; set; }
        private UserRoles()
        {

        }
        public UserRoles(Role role, User user)
        {
            Role = role;
            User = user;
        }
        public UserRoles(long RoleId, long UserId)
        {
            this.UserId = UserId;
            this.RoleId = RoleId;
        }

    }
}
