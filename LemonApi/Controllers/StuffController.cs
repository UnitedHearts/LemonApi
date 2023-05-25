using LemonApi.Extansions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StuffController : LemonController
    {
        public StuffController(LemonDbContext dbContext) : base(dbContext)
        {
        }

        [HttpPost("Buy")]
        public async Task<Account> Buy(Guid id)
        {
            var stuff = _db.Stuffs.FirstOrDefault(e => e.Id == id);
            if(stuff == null) throw new Exception("Предмет не найден в магазине");
            if (ContextUser.Stuffs.Contains(stuff)) throw new Exception("Предмет уже куплен");
            if (ContextUser.Cash.Current < stuff.Price) throw new Exception("Недостаточно монет");
            new CashBuilder(new AccountBuilder(ContextUser).AddStuff(stuff).Build().Cash).AddCash(-stuff.Price);
            await _db.SaveChangesAsync();
            return ContextUser;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Stuff>> Get()
        {
            return _db.Stuffs.Where(s => !ContextUser.Stuffs.Contains(s));
        }

        [HttpGet("Get/{type}")]
        public async Task<IEnumerable<Stuff>> GetByType(string type)
        {
            return _db.Stuffs.Where(s => !ContextUser.Stuffs.Contains(s) && s.Type == type);
        }
    }
}
