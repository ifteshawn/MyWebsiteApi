using MyWebsiteApi.Models;
using Resend;

namespace MyWebsiteApi.Services
{
    public class MailDataService : IMailDataService
    {
        private readonly IResend _resend;
        public MailDataService(IResend resend)
        {
            _resend = resend;
        }

        public async Task<bool> SendEmailAsync(MailData mailData)
        {
            var message = new EmailMessage()
            {
                From = "onboarding@resend.dev",
                To = mailData.SenderEmail ?? throw new ArgumentNullException(nameof(mailData.SenderEmail)),
                // message.Subject = "Hello!";
                Subject = mailData.Subject ?? throw new ArgumentNullException(nameof(mailData.Subject)),
                // message.HtmlBody = "<div><strong>Greetings<strong> 👋🏻 from .NET</div>";
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