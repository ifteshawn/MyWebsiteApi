using MyWebsiteApi.Models;

namespace MyWebsiteApi.Services
{
    public interface IMailDataService
    {
        Task<bool> SendEmailAsync(MailData mailData);
    }
}