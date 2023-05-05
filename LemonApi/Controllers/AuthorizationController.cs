using Contracts.Http;
using Contracts.Mail;
using LemonApi.Extensions;
using LemonDB;
using LemonDB;
using Microsoft.AspNetCore.Mvc;

namespace LemonApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController
{
    readonly LemonDbContext _db;
    readonly IMailSender _mailService;

    public AuthorizationController(LemonDbContext db, IMailSender mailSender)
    {
        _db = db;
        _mailService = mailSender;
    }

    [HttpPost("Login")]
    public async Task<Answer<Account>> Login(LoginModel login)
    {
        login.Validate();
        var acc = _db.Accounts.FirstOrDefault(e => e.Email == login.Email && e.Password == login.Password);
        if (acc == null)
            throw new Exception("Неврные логин или пароль");
        if (!acc.EmailConfirmed)
        {
            var mail = MailExtansion.ConfirmMail(acc.Email);
            await _mailService.SendAsync(mail);
            throw new Exception("Почта не была подтверждена. Направили письмо для подтверждение адреса электронной почты");
        }
        return new(acc);
    }
}
