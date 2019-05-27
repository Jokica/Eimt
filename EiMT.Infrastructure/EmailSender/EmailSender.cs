using Eimt.Application.Interfaces;
using Eimt.Application.Interfaces.Models.EmailModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EiMT.Infrastructure.EmailSender
{
    public class EmailSender : IMessageSender
    {
        private readonly IOptions<EmailSenderConfiguration> options;

        public EmailSender(IOptions<EmailSenderConfiguration> options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
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
            foreach (var attacment in attacments)
            {
                if (attacment.IsStreamable)
                {
                    body.Attachments.Add(attacment.FileName, attacment.Stream);
                }
            }
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = ValidationCallBack;
                await client.ConnectAsync("smtp.live.com", 587, false);
                await client.AuthenticateAsync(options.Value.Email, options.Value.Password);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        private bool ValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
