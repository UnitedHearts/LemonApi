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

        [HttpGet("Get")]
        public async Task<Account> Get(Guid value)
        {
            return await _db.Accounts.FirstOrDefaultAsync(e => e.Id == value);
        }
        [HttpPost("Post")]
        public async Task Post(Registration data)
        {
            var account = new AccountBuilder()
                                            .SetEmail(data.Email)
                                            .SetName(data.Name)
                                            .SetPassword(data.Password)
                                            .Build();
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }
        [HttpPost("Post2")]
        public async Task Post(string email, string name, string password)
        {
            var account = new AccountBuilder()
                                            .SetEmail(email)
                                            .SetName(name)
                                            .SetPassword(password)
                                            .Build();
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }
        [HttpPut("Update")]
        public async Task Update(Registration data)
        {
            var account = new AccountBuilder()
                                            .SetEmail(data.Email)
                                            .SetName(data.Name)
                                            .SetPassword(data.Password)
                                            .Build();
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();

        }
    }
}