using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyForum.Core.Services.Interface;
using MyForum.Core.Settings;
using MyForum.Models.Helpers.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyForum.Core.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        private readonly SendGridClient emailClient;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
            this.emailClient = new SendGridClient(this.emailSettings.ApiKey);
        }

        public async Task<bool> Send(EmailMessage emailMessage)
        {
            var emailContentParams = new EmailContentParams(emailSettings.Sender, emailMessage.Email);

            var email = MailHelper.CreateSingleEmail(emailContentParams.FromAddress, emailContentParams.ToAddress, emailMessage.Subject, emailMessage.Message, emailMessage.Message);

            var response = await emailClient.SendEmailAsync(email);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}