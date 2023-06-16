using LemonApi.Extansions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [HttpGet("Get")]
    public async Task<IEnumerable<Session>> GetAll()
    {
        return await _db.Sessions.Include(e => e.Participants)
                                    .Include(e => e.Map)
                                    .ToListAsync();
    }

    [HttpGet("Get/{id}")]
    public async Task<Session> Get(Guid id)
    {
        return await _db.GetSessionAsync(id);
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
        var accounts = data.Participants?.Select(e => _db.Accounts.FirstOrDefault(a => a.Email == e.Email));
        var session = await _db.GetSessionAsync(Guid.Parse(data.SessionId));
        switch (SessionStateExtansion.ToSessionState(data.State))
        {
            case SessionState.PENDING:
                break;
            case SessionState.PLAYING:
                if(data.Participants is not null) session.Participants = accounts.ToList();
                session.State = SessionState.PLAYING.ToString();
                break;
            case SessionState.OVER:
                PlayerSessionStatBuilder statBuilder;
                 var participants = CalculateRank(data.Participants, session.Duration);
                foreach (var participant in participants)
                {
                    var acc = await _db.GetAccountAsync(participant.Email);
                    var stat = await _db.PlayersSessionsStats.FirstOrDefaultAsync(e => e.Session.Id == session.Id && e.Account.Id == acc.Id);

                    var exp = (int)GetScore(participant, session.Duration);
                    exp = exp > 0 ? exp : 0;
                    statBuilder = stat is null ? new() : new(stat);
                    new CashBuilder(acc.Cash).AddCash(participant.Coins - (stat is null ? 0 : stat.Coins));

                    stat = statBuilder.SetRank(participant.Rank)
                                    .SetSpawnTimePoint(participant.SpawnTimePoint)
                                    .SetDeadTimePoint(participant.DeadTimePoint ?? 0)
                                    .SetCoins(participant.Coins)
                                    .SetFails(participant.Fails)
                                    .SetPunches(participant.Punches)
                                    .SetExp(exp)
                                    .SetSession(session)
                                    .SetAccount(acc)
                                    .Build();
                    if (stat.Id == Guid.Empty)
                        _db.PlayersSessionsStats.Add(stat);
                    acc.Statistic.Plays++;
                    acc.Statistic.Exp += exp;
                    if (participant.Rank == 1) acc.Statistic.Wins++;
                    if (participant.DeadTimePoint != 0) acc.Statistic.Deaths++;
                }
                session.State = SessionState.OVER.ToString();
                break;
        }
        await _db.SaveChangesAsync();
        return session;
    }

    [HttpGet("LastSession")]
    public async Task<Session> GetLastSession()
    {
        var session = await _db.Sessions
                                    .OrderByDescending(s => s.Date)
                                    .Include(s => s.Map)
                                    .Include(s => s.Participants)
                                    .FirstOrDefaultAsync(s => s.Participants.Contains(ContextUser));
        if (session is null) throw new Exception("Игрок не сыграл ни одной игры");
        return session;
    }
    [HttpGet("Statistic")]
    public async Task<PlayerSessionStat> GetStatistic(Guid sessionId)
    {
        var stat = await _db.PlayersSessionsStats.IgnoreAutoIncludes().FirstOrDefaultAsync(e => e.Session.Id == sessionId && e.Account == ContextUser);
        if (stat is null) throw new Exception("Статистика по указанной сессиии не найдена");
        return stat;
    }
    [HttpGet("RatingTable")]
    public async Task<IEnumerable<PlayerSessionStat>> GetRatingTable(Guid sessionId)
    {
        return _db.PlayersSessionsStats.Include(s => s.Account).Where(s => s.Session.Id == sessionId).OrderBy(s => s.Rank).AsEnumerable();
    }

    IEnumerable<PlayerResult> CalculateRank(IEnumerable<PlayerResult> results, double sessionTime)
    {
        return results.Select(r => (Stat: r, Score: GetScore(r, sessionTime))).OrderByDescending(s => s.Score).Select((r, index) => { r.Stat.Rank = index + 1; return r.Stat; });
    }
    double GetScore(PlayerResult result, double sessionTime)
    {
        var m_coin = 5;
        var m_punches= 2;
        var m_time= 0.5;
        var m_fails = 3;
        var m_alive = 1.2;
        var isAlive = result.DeadTimePoint is null;

        double s_coin = 50 + result.Coins * m_coin < 500 ? 50 + result.Coins * m_coin : 500;
        double s_punches = result.Punches * m_punches < 200 ? result.Punches * m_punches : 200;
        double s_time = isAlive ? sessionTime * m_time : (double)result.DeadTimePoint * m_time;
        double s_fails = result.Fails * m_fails < 300 ? result.Fails* m_fails : 300;

        var total = (s_coin + s_punches + s_time - s_fails) * (isAlive ? m_alive : 1);
        return total > 0 ? total : new Random().Next(10, 25); 
    }
}
