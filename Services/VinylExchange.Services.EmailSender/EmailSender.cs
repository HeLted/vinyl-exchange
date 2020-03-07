using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace VinylExchange.Services.EmaiSender
{
    public class EmailSender : IEmailSender
    {
        private const string SENDER_EMAIL = "no-reply@.com";
        private const string NAME_OF_THE_SENDER = "Vinyl Exchange";  

        public EmailSender(string sendGridId)
        {
            this.SendGridKey = sendGridId;    
        }

        public string SendGridUser { get; set; }

        public string SendGridKey { get; set; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(this.SendGridKey, subject, message, email);
        }

        public async  Task Execute(string apiKey, string subject, string message, string email)
        {          
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("no-reply@vinylexchange.com", "VinylExchangeAdmin");
           
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = String.Empty;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
