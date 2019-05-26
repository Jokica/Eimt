using Eimt.Application.Interfaces.Models.EmailModels;
using System.Threading.Tasks;

namespace Eimt.Application.Interfaces
{
    public interface IMessageSender
    {
        Task SendMail(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attacment[] attacments
            );

    }
}