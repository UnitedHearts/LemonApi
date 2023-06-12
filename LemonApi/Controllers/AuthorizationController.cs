using Contracts.Http;
using Contracts.Mail;
using LemonApi.Extansions;
using LemonApi.Extensions;
using LemonApi.Models;
using LemonDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LemonApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : LemonController
{
    readonly IMailSender _mailService;
    readonly JWTConfig _jwtConfig;
    readonly Envelope _env;

    public AuthorizationController(LemonDbContext db, IMailSender mailSender, JWTConfig jwtConfig, Envelope env) : base(db)
    {
        _mailService = mailSender;
        _jwtConfig = jwtConfig;
        _env = env;
    }

    [HttpPost("Login")]
    public async Task<string> Login(LoginModel login)
    {
        login.Validate();
        var acc = _db.Accounts.FirstOrDefault(e => e.Email == login.Email && e.Password == login.Password);
        if (acc is null)
            throw new Exception("Неврные логин или пароль");
        if (!acc.EmailConfirmed)
        {
            var mail = MailExtansion.ConfirmMail(acc.Email, _env.Host, acc.Name);
            await _mailService.SendAsync(mail);
            throw new Exception("Почта не была подтверждена. Направили письмо для подтверждение адреса электронной почты");
        }
        var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, acc.Name),
                new Claim(ClaimTypes.Email, acc.Email),
                new Claim(ClaimTypes.Role, ((Contracts.Account.Roles)acc.Role).ToString())
            };

        return JWTExtansion.GetToken(claims, _jwtConfig);
    }
    //[Authorize]
    [HttpGet("WhoIAm")]
    public async Task<Account> WhoIAm()
    {
        return ContextUser;
    }
}
