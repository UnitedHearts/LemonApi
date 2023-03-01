using LemonDB;
using LemonDB.Builders;
using LemonDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        readonly LemonDbContext _db;

        public AccountController(LemonDbContext db)
        {
            _db = db;
        }

        [HttpGet("Hello")]
        public async Task<string> Hello()
        {
            return "Hello World!";
        }

        [HttpGet("Get")]
        public async Task<Account> Get(Guid value)
        {
            return await _db.Accounts.FirstOrDefaultAsync(e => e.Id == value);
        }
        [HttpPost("Post")]
        public async Task Post(CreateInfo data)
        {
            var account = new AccountBuilder()
                                            .SetEmail(data.Email)
                                            .SetName(data.Name)
                                            .SetPassword(data.Password)
                                            .Build();
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }
        [HttpPut("Update")]
        public async Task Update(UpdateInfo data)
        {
            var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == data.Id);
            if (old == null) return;
            var account = new AccountBuilder(old)
                                            .SetEmail(data.Email)
                                            .SetName(data.Name)
                                            .SetPassword(data.Password)
                                            .Build();
            await _db.SaveChangesAsync();
        }
        [HttpDelete("Delete")]
        public async Task<bool> Delete(Guid id)
        {
            var old = await _db.Accounts.FirstOrDefaultAsync(e => e.Id == id);
            if (old == null) return false;
            _db.Accounts.Remove(old);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}