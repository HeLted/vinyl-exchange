namespace VinylExchange.Services.EmaiSender
{
    #region

    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    #endregion

    public class EmailSender : IEmailSender
    {
        private const string NameOfTheSender = "Vinyl Exchange Support";

        private const string SenderEmail = "no-reply@vinylexchange.com";

        public EmailSender(string sendGridId)
        {
            this.SendGridKey = sendGridId;
        }

        public string SendGridKey { get; set; }

        public string SendGridUser { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return this.Execute(this.SendGridKey, subject, message, email);
        }

        private async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(SenderEmail, NameOfTheSender);

            var to = new EmailAddress(email);
            var plainTextContent = string.Empty;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}