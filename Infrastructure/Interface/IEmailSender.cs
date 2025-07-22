namespace Infrastructure.Interface
{
    public interface IEmailSender
    {
        Task SendResetLinkAsync(string toEmail, string link);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
