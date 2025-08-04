using Domain.Models;
using Infrastructure.Interface;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly EmailSettings _settings;

        public EmailSender(IConfiguration config, IOptions<EmailSettings> settings)
        {
            _config = config;
            _settings = settings.Value;
        }

        public async Task SendResetLinkAsync(string toEmail, string link)
        {
            var message = new MimeMessage
            {
                Subject = "Password Reset",
                Body = new TextPart("plain")
                {
                    //Text = $"Click the link to reset your password: {link}"
                    Text = link
                }
            };

            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var smtp = new System.Net.Mail.SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}