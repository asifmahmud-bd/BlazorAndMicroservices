using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSetting _emailSetting { get;}
        public ILogger<EmailService> _logger { get;}

        public EmailService(IOptions<EmailSetting> emailSetting, ILogger<EmailService> logger)
        {
            _logger = logger;
            _emailSetting = emailSetting.Value;
        }

        public async Task<bool> SendEmail(Email email)
        {
            //SendGrid
            var client = new SendGridClient(_emailSetting.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);

            var response = await client.SendEmailAsync(sendGridMessage);

            if(response.StatusCode == HttpStatusCode.Accepted)
            {
                _logger.LogError("Email send ok");

                return true;
            }

            _logger.LogError("Email sending error");

            return false;

        }
    }
}
