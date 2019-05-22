using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Models.EmailModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EiMT.Infrastructure.EmailSender
{
    public class EmailSender : IMessageSender
    {
        private readonly IUrlHelper Url;
        private readonly EmailSenderConfiguration Configuration;
        private readonly HttpContext context;

        public EmailSender(IUrlHelperFactory factory,
            IActionContextAccessor actionContextAccessor,
            IHttpContextAccessor contextAccessor,
            IOptions<EmailSenderConfiguration> options)
        {
            Url = factory.GetUrlHelper(actionContextAccessor.ActionContext);
            Configuration = options.Value;
            context = contextAccessor.HttpContext;
        }
        public Task SendConfirmationToken(string email, string securityStamp)
        {
            string url = CreateConfirmationTokenUrl(email, securityStamp);
            string body = CreateConfirmationTokenBody(email,url);
            return SendMail(Configuration.DisplayName,
                            Configuration.Email,
                            email,
                            email,
                            "Confirmation Token",
                            body);
        }

        private string CreateConfirmationTokenBody(string email, string url)
        {
            var name = email.Split('.').FirstOrDefault();
                return  $@"Dear {name}  
                        Place click <a href='{url}'>here</a> to complete registration 
                        From EiMT";
        }

        private string CreateConfirmationTokenUrl(string email, string securityStamp)
        {
            return Url.Action(
                "ConfirmEmail",
                "User",
                new { email, token = securityStamp },
                context.Request.Scheme);
        }

        public async Task SendMail(
            string fromDisplayName, 
            string fromEmailAddress, 
            string toName, 
            string toEmailAddress, 
            string subject, 
            string message, 
            params Attacment[] attacments)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message,
            };
            email.Body = body.ToMessageBody();
            foreach(var attacment in attacments)
            {
                if (attacment.IsStreamable)
                {
                    body.Attachments.Add(attacment.FileName, attacment.Stream);
                }
            }
            using(var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = ValidationCallBack;

                await client.ConnectAsync("smtp.live.com", 587, false);
                await client.AuthenticateAsync(Configuration.Email, Configuration.Password);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        private bool ValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public Task SendResetPasswordEmail(string email, string v, string generatedPassword)
        {
            return SendMail(
                Configuration.DisplayName,
                Configuration.Email,
                "",
                email,
                "Password Resset",
                v);
        }

        public Task SendResetPasswordEmail(string email, string generatedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
