using Contracts.Http;
using Contracts.Mail;
using LemonApi.Extensions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LemonApi.Extansions;

namespace LemonApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : LemonController
{
    readonly IMailSender _mailService;
    readonly Envelope _env;

    public AccountController(LemonDbContext db, IMailSender mailSender, Envelope env) : base(db)
    {
        _mailService = mailSender;
        _env = env;
    }

    [HttpGet("Get/{id}")]
    public async Task<Account> Get(Guid id)
    {
        var acc = await _db.Accounts
                            .Include(e => e.Statistic)
                            .Include(e => e.Sessions)
                            .Include(e => e.Stuffs)
                            .Include(e => e.Cash)
                            .FirstOrDefaultAsync(e => e.Id == id);
        if (acc == null)
            throw new Exception("Аккаунт не найден");

        return acc;
    }

    [HttpGet("Get")]
    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _db.Accounts
                            .Include(e => e.Statistic)
                            .Include(e => e.Sessions)
                            .Include(e => e.Stuffs)
                            .Include(e => e.Cash)
                            .ToListAsync();
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<Account> Create(AccountCreateInfo data)
    {
        data.Validate();
        if (_db.Accounts.FirstOrDefault(e => e.Email == data.Email) != null) 
            throw new Exception("Аккаунт с указанным Email уже существует. Пожалуйста, проверьте почту напредмет письма-подтверждения");
        var account = new AccountBuilder()
                            .SetEmail(data.Email)
                            .SetName(data.Name)
                            .SetPassword(data.Password)
                            .SetState(AccountState.OFFLINE.ToString())
                            .Build();
        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();

        var mail = MailExtansion.ConfirmMail(data.Email, _env.Host);
        await _mailService.SendAsync(mail);
        return account;
    }

    [HttpPut("Update")]
    public async Task<Account> Update(AccountUpdateInfo data)
    {
        data.Validate();
        var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == data.Id);
        if (old == null)
            throw new Exception("Изменяемый аккаунт не найден");

        var account = new AccountBuilder(old)
                            .SetEmail(data.Email)
                            .SetName(data.Name)
                            .SetPassword(data.Password)
                            .Build();
        await _db.SaveChangesAsync();
        return account;
    }

    [Authorize(Roles = "Admin, Server")]
    [HttpPut("Disable")]
    public async Task<string> Disable(Guid id)
    {
        var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id);
        if (old == null)
            throw new Exception("Деактивируемый аккаунт не найден");

        var account = new AccountBuilder(old).Disable();
        await _db.SaveChangesAsync();
        return RequestStatus.SUCCESS;
    }

    [AllowAnonymous]
    [HttpGet("Confirm")]
    public async Task<ContentResult> Confirm(string token)
    {
        var claims = JWTExtansion.ValidateToken(token);
        var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (email == null)
            throw new Exception("Недействительный токен");

        var old = await _db.GetAccountAsync(email);
        if (old == null)
            throw new Exception("Аккаунт не найден");

        if (old.EmailConfirmed)
            throw new Exception("Аккаунт не найден");
        var freeCharacter = _db.Stuffs.FirstOrDefault(s => s.GameKey == "Character_Duck");
        var account = new AccountBuilder(old)
                            .Confirmed()
                            .Active()
                            .SetCash()
                            .SetStatistic()
                            .AddStuff(freeCharacter);
        await _db.SaveChangesAsync();
        return base.Content("<div>SUCCESS</div>", "text/html");

    }

    [Authorize(Roles = "Admin, Server")]
    [HttpDelete("Delete")]
    public async Task<string> Delete(Guid id)
    {
        var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id);
        if (old == null)
            throw new Exception("Удаляемый аккаунт не найден");

        _db.Accounts.Remove(old);
        await _db.SaveChangesAsync();
        return RequestStatus.SUCCESS;
    }
}