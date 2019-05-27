using Eimt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace EiMT.Infrastructure.EmailSender
{
    public class UserEmailSender : EmailSender, IUserMessageSender
    {
        private readonly IUrlHelper Url;
        private readonly EmailSenderConfiguration Configuration;
        private readonly HttpContext context;
        public UserEmailSender(
            IUrlHelperFactory factory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor contextAccessor,
            IOptions<EmailSenderConfiguration> options) : base(options)
        {
            Url = factory.GetUrlHelper(actionContextAccessor.ActionContext);
            Configuration = options.Value;
            context = contextAccessor.HttpContext;
        }
        public Task SendConfirmationToken(string email, string securityStamp)
        {
            var name = email.Split('.').FirstOrDefault();
            string url = CreateConfirmationTokenUrl(email, securityStamp);
            string body = CreateConfirmationTokenBody(email, url);
            return SendMail(Configuration.DisplayName,
                             Configuration.Email,
                            name,
                            email,
                            "Confirmation Token",
                            body);
        }

        private string CreateConfirmationTokenBody(string email, string url)
        {
            var name = email.Split('.').FirstOrDefault();
            return $@"Dear {name}  <br/>
                        Place click <a href='{url}'>here</a> to complete registration<br/> <br/>
                        From EiMT-Lab";
        }

        private string CreateConfirmationTokenUrl(string email, string securityStamp)
        {
            return Url.Action(
                "ConfirmEmail",
                "User",
                new { email, token = securityStamp },
                context.Request.Scheme);
        }
        private string CreateResetPasswordBody(string email, string generatedPassword)
        {
            var name = email.Split('.').FirstOrDefault();
            return $@"Dear {name}  <br/>
                        You requested to change your password.Here is your new password {generatedPassword}<br/> <br/>
                        From EiMT-Lab";
        }

        public Task SendResetPasswordEmail(string email, string generatedPassword)
        {
            var name = email.Split('.').FirstOrDefault();
            return SendMail(
               Configuration.DisplayName,
               Configuration.Email,
               name,
               email,
               "Password Resset",
               CreateResetPasswordBody(email, generatedPassword));
        }
    }
}
