using System.Configuration;
using MyWebsiteApi.Models;
using Resend;

namespace MyWebsiteApi.Services
{
    public class MailDataService : IMailDataService
    {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;
        public MailDataService(IResend resend, IConfiguration configuration)
        {
            _resend = resend;
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(MailData mailData)
        {
            var message = new EmailMessage()
            {
                From = "info@brainwreck.co",
                To = _configuration["MailSettings:ReceiverEmail"] ?? throw new ArgumentNullException("Receiver email is missing."),
                ReplyTo = mailData.SenderEmail ?? throw new ArgumentNullException(nameof(mailData.SenderEmail)),
                Subject = mailData.Subject ?? throw new ArgumentNullException(nameof(mailData.Subject)),
                HtmlBody = mailData.Message ?? throw new ArgumentNullException(nameof(mailData.Message))
            };

            try
            {
                await _resend.EmailSendAsync(message);
            }
            catch (Exception)
            {
                Console.WriteLine("Error sending email");
                return false;
            }

            return true;
        }
    }
}