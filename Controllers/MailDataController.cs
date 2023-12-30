using Microsoft.AspNetCore.Mvc;
using MyWebsiteApi.Models;
using MyWebsiteApi.Services;

namespace MyWebsiteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailDataController : ControllerBase
    {
        private readonly IMailDataService _mailDataService;
        public MailDataController(IMailDataService mailDataService)
        {
            _mailDataService = mailDataService;
        }

        //POST: api/MailData
        [HttpPost]
        public async Task<IActionResult> SendEmail(MailData mailData)
        {
            try
            {
                await _mailDataService.SendEmailAsync(mailData);
                return Ok(mailData);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while sending email.");
            }
        }
    }
}