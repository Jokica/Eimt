using System;

namespace Eimt.Domain.DomainModels
{
    public class UserConfirmationToken
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreateAt { get; set; }

        public UserConfirmationToken(User user, string stamp)
        {
            User = user;
            SecurityStamp = stamp;
            CreateAt = DateTime.Now;
        }
        private UserConfirmationToken()
        {

        }

    }
}
