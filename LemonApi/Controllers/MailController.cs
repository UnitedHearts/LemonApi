using Contracts.Mail;
using LemonDB.Models;
using MailKit;
using Microsoft.AspNetCore.Mvc;

namespace LemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController
    {
        public readonly IMailSender _mailService;

        public MailController(IMailSender mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task Get(MailInfo mail)
        {
            await _mailService.SendAsync(mail);
        }
    }
}
