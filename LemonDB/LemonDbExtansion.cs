using Microsoft.EntityFrameworkCore;

namespace LemonDB;

public static class LemonDbExtansion
{
    public static async Task<Session> GetSessionAsync(this LemonDbContext _db, Guid id)
    {
        return await _db.Sessions.Include(e => e.Participants)
                                    .FirstOrDefaultAsync(e => e.Id == id)
                                    ?? throw new Exception("Сессия не найдена");
    }
    public static async Task<Map> GetMapAsync(this LemonDbContext _db, Guid id)
    {
        return await _db.Maps.FirstOrDefaultAsync(e => e.Id == id)
                                    ?? throw new Exception("Карта не найдена");
    }
    public static async Task<Account> GetAccountAsync(this LemonDbContext _db, string email)
    {
        return await _db.Accounts.Include(e => e.Statistic)
                                    .Include(e => e.Sessions).ThenInclude(e => e.Map)
                                    .Include(e => e.Stuffs)
                                    .Include(e => e.Cash)
                                    .FirstOrDefaultAsync(e => e.Email == email)
                                    ?? throw new Exception("Аккаунт не найден");
    }
}
