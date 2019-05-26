using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eimt.Application.Interfaces
{
    public interface IUserMessageSender:IMessageSender
    {
        Task SendResetPasswordEmail(string email, string generatedPassword);
        Task SendConfirmationToken(string email, string securityStamp);
    }
}
