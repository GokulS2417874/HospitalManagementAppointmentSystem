namespace Infrastructure.Interface
{
    public interface IEmailSender
    {
        Task SendResetLinkAsync(string toEmail, string link);
    }
}
