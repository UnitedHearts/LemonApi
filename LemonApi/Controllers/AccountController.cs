using Contracts.Mail;
using EmailService;
using LemonApi.Extensions;
using LemonDB;
using LemonDB.Builders;
using LemonDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        readonly LemonDbContext _db;
        readonly IMailSender _mailService;

        public AccountController(LemonDbContext db, IMailSender mailSender)
        {
            _db = db;
            _mailService = mailSender;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Account>> Get(Guid id)
        {
            var acc = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id);
            if (acc == null) throw new Exception("Аккаунт не найден");
            return acc;
        }
        [HttpGet("Get")]
        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _db.Accounts.ToListAsync();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateInfo data)
        {
            data.Validate();
            if (_db.Accounts.FirstOrDefault(e => e.Email == data.Email) != null) throw new Exception("Аккаунт с указанным Email уже существует. Пожалуйста, проверьте почту напредмет письма-подтверждения");
            var account = new AccountBuilder()
            .SetEmail(data.Email)
            .SetName(data.Name)
            .SetPassword(data.Password)
            .Build();
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();

            var mail = MailExtansion.ConfirmMail(data.Email);
            await _mailService.SendAsync(mail);
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateInfo data)
        {
            data.Validate();
            var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == data.Id);
            if (old == null) throw new Exception("Изменяемый аккаунт не найден");
            var account = new AccountBuilder(old)
            .SetEmail(data.Email)
            .SetName(data.Name)
            .SetPassword(data.Password);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("Confirm")]
        public async Task<IActionResult> Confirm(string token)
        {
            var claims= JWTExtansion.ValidateToken(token);
            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Email == email);
            if (old == null) throw new Exception("Аккаунт не найден");
            if (old.EmailConfirmed) return Ok("Почта уже подтверждена");
            var account = new AccountBuilder(old).Confirmed().Active();
            await _db.SaveChangesAsync();
            return Ok("Почта подтверждена");
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id);
            if (old == null) throw new Exception("Удаляемый аккаунт не найден");
            var account = new AccountBuilder(old).Disable();
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}