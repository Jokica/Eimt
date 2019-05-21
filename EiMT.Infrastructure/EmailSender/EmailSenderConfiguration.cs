using System;
using System.Collections.Generic;
using System.Text;

namespace EiMT.Infrastructure.EmailSender
{
    public class EmailSenderConfiguration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
