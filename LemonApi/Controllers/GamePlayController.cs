using LemonApi.Extansions;
using LemonApi.Extensions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GamePlayController : LemonController
{
    public GamePlayController(LemonDbContext dbContext) : base(dbContext)
    {
    }

    [HttpPost("Sessions/Search")]
    public async Task<Session> Search(SearchData searchData)
    {
        var map = await _db.Maps.FirstOrDefaultAsync(map => map.Id == searchData.MapId);
        if (map is null) throw new Exception("Указанная карта не найдена");
        var session = _db.Sessions
                            .Include(s => s.Map)
                            .Include(s => s.Participants)
                            .FirstOrDefault(s => s.Map.Id == map.Id && (s.State == SessionState.AWAIT.ToString() || s.State == SessionState.PENDING.ToString()) && s.StartPlayersCount < map.MaxPlayersCount);
        if (session == null)
        {
            session = new SessionBuilder()
                                .SetState(SessionState.AWAIT.ToString())
                                .SetStartPlayersCount(1)
                                .SetMap(map)
                                .SetDuration(searchData.Duration)
                                .SetKey("")
                                .SetDate(DateTime.Now)
                                .SetParticipants(new List<Account>() { ContextUser })
                                .Build();
            _db.Sessions.Add(session);
        }
        else
        {
            new SessionBuilder(session)
                    .AddParticipant(ContextUser);
        }
        await _db.SaveChangesAsync();
        return session;
    }

    [HttpPost("Sessions/Status")]
    public async Task<Session> Status(Guid id)
    {
        var session = await _db.Sessions.FirstOrDefaultAsync(e => e.Id == id);
        if (session is null) throw new Exception("Сессия не найдена");
        return session;
    }

    [HttpPost("Sessions/StopSearch")]
    public async Task<string> StopSearch(Guid id)
    {
        var session = await _db.Sessions
                                    .Include(e => e.Participants)
                                    .FirstOrDefaultAsync(e => e.Id == id && (e.State == SessionState.AWAIT.ToString() || e.State == SessionState.PENDING.ToString()));
        if (session is null) return "Сессия не найдена";
        new SessionBuilder(session).RemoveParticipant(ContextUser);
        await _db.SaveChangesAsync();
        return "SUCCESS";
    }

    [Authorize(Roles ="Server")]
    [HttpPost("Sessions/Process")]
    public async Task<Session> ProcessSession(string host)
    {
        var session = await _db.Sessions.FirstOrDefaultAsync(e => e.State == SessionState.AWAIT.ToString() && e.StartPlayersCount > 0);
        if (session is null) throw new Exception("Сессий не найдено");
        new SessionBuilder(session).SetState(SessionState.PENDING.ToString()).SetKey(host);
        await _db.SaveChangesAsync();
        return session;
    }

    [HttpGet("Maps/Get")]
    public async Task<IEnumerable<Map>> GetMaps()
    {
        return await _db.Maps.ToListAsync();
    }

}
