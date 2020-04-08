namespace VinylExchange.Services.EmailSender
{
    #region

    using System.Threading.Tasks;

    #endregion

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}