using LemonApi.Extansions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LemonApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SessionController : LemonController
{
    public SessionController(LemonDbContext db) : base(db)
    {
    }

    [HttpGet("Get/{id}")]
    public async Task<Session> Get(Guid id)
    {
        return await _db.GetSessionAsync(id);
    }

    [HttpGet("Get")]
    public async Task<IEnumerable<Session>> GetAll()
    {
        return await _db.Sessions.Include(e => e.Participants)
                                    .Include(e => e.Map)
                                    .ToListAsync();
    }

    [HttpPost("Create")]
    public async Task<Session> Create(CreateSessionData data)
    {
        var emails = data.Participants.Select(e => e.Email);
        var session = new SessionBuilder()
                                .SetMap(_db.Maps.FirstOrDefault(e => e.Id.ToString() == data.MapId) ?? throw new ArgumentException("Карта не найдена"))
                                .SetStartPlayersCount(data.StartPlayersCount)
                                .SetState(SessionState.PENDING.ToString())
                                .SetKey(data.GameKey)
                                .SetDate(DateTime.Now)
                                .SetDuration(data.Duration)
                                .SetParticipants(_db.Accounts.Where(e => emails.Contains(e.Email)).ToList())
                                .Build();
        _db.Sessions.Add(session);
        await _db.SaveChangesAsync();
        return session;
    }

    [HttpPut("Update")]
    public async Task<Session> Update(SessionUpdateData data)
    {
        var accounts = data.Participants.Select(e => _db.Accounts.FirstOrDefault(a => a.Email == e.Email));
        var session = await _db.GetSessionAsync(Guid.Parse(data.SessionId));
        switch (SessionStateExtansion.ToSessionState(data.State))
        {
            case SessionState.PENDING:
                break;
            case SessionState.PLAYING:
                session.Participants = accounts.ToList();
                break;
            case SessionState.OVER:
                PlayerSessionStatBuilder statBuilder;
                foreach (var participant in data.Participants)
                {
                    var acc = await _db.GetAccountAsync(participant.Email);
                    var stat = await _db.PlayersSessionsStats.FirstOrDefaultAsync(e => e.Session.Id == session.Id && e.Account.Id == acc.Id);
                    
                    statBuilder = stat is null ? new() : new(stat);
                    new CashBuilder(acc.Cash).AddCash(participant.Coins - (stat is null ? 0 : stat.Coins));

                    stat = statBuilder.SetRank(participant.Rank)
                                    .SetSpawnTimePoint(participant.SpawnTimePoint)
                                    .SetDeadTimePoint(participant.DeadTimePoint ?? 0)
                                    .SetCoins(participant.Coins)
                                    .SetFails(participant.Fails)
                                    .SetPunches(participant.Punches)
                                    .SetSession(session)
                                    .SetAccount(acc)
                                    .Build();
                    if (stat.Id == Guid.Empty)
                        _db.PlayersSessionsStats.Add(stat);
                }
                session.State = SessionState.OVER.ToString();
                break;
        }
        await _db.SaveChangesAsync();
        return session;
    }
}
