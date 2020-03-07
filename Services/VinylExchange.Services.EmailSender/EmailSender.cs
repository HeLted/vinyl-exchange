namespace VinylExchange.Services.EmaiSender
{
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSender
    {
        private const string SenderEmail = "no-reply@vinylexchange.com";

        private const string NameOfTheSender = "Vinyl Exchange Support";

        public EmailSender(string sendGridId)
        {
            this.SendGridKey = sendGridId;
        }

        public string SendGridKey { get; set; }

        public string SendGridUser { get; set; }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress(SenderEmail, NameOfTheSender);

            EmailAddress to = new EmailAddress(email);
            string plainTextContent = string.Empty;
            string htmlContent = message;
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return this.Execute(this.SendGridKey, subject, message, email);
        }
    }
}