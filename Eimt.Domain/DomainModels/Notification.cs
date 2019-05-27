using System;

namespace Eimt.Domain.DomainModels
{
    public class Notification
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string From { get; set; }
        private Notification()
        {

        }
        public Notification(string message, User user, string from)
        {
            Message = message;
            User = user;
            CreatedAt = DateTime.Now;
            From = from;
        }
        public Notification(string message, long usrrid, string from)
        {
            Message = message;
            UserId = usrrid;
            CreatedAt = DateTime.Now;
            From = from;
        }
    }
}
