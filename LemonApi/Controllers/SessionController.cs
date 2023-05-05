using Contracts.Http;
using LemonApi.Models;
using LemonDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    readonly LemonDbContext _db;
    public SessionController(LemonDbContext db)
    {
        _db = db;
    }
    [HttpGet("Get/{id}")]
    public async Task<Answer<Session>> Get(Guid id)
    {
        var sess = await _db.Sessions.FirstOrDefaultAsync(e => e.Id == id);
        if (sess == null) throw new Exception("Сессия не найдена");
        return new(sess);
    }
    [HttpGet("Get")]
    public async Task<Answer<IEnumerable<Session>>> GetAll()
    {
        return new(await _db.Sessions.ToListAsync());
    }
    [HttpPost("Create")]
    public async Task<Answer<Session>> Create(SessionData data)
    {
        foreach(var part in data.Participants)
        {
            var stat = _db.Statistics.FirstOrDefault(e => e.Account.Email == part.Email);
            stat.TotalMoney += part.Coins;
            stat.Money += part.Coins;
            if(part.Dead)
                stat.Deaths++;
            else
                stat.Wins++;
        }

        return new(new Session());
    }
}
