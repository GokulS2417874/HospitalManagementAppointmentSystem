using Infrastructure.Interface;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Infrastructure.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        private class SmtpSettings
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string User {  get; set; }
            public string Password { get; set; }
            public string FromEmail { get; set; }   
            public string FromName { get; set; }
        }

        public async Task SendResetLinkAsync(string toEmail, string Link)
        {
            var settings = _config.GetSection("smtp").Get<SmtpSettings>();

            var message = new MimeMessage
            {
                Subject = "Password Reset",
                Body = new TextPart("plain")
                {
                    Text = $"Click the link to reset your Password ;{Link}"
                }
            };

            message.From.Add(new MailboxAddress(settings.FromName, settings.FromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));


            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(settings.User, settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }
    }
}
