using LemonDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LemonApi.Extansions;

public abstract class LemonController : ControllerBase
{
    protected readonly LemonDbContext _db;
    public LemonController(LemonDbContext dbContext)
    {
        _db = dbContext;
    }
    protected Account ContextUser
    {
        get
        {
            var email = HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value;
            if (email is null) throw new Exception("Невалидный токен");
            var account = _db.Accounts.Include(a => a.Cash)
                                        .Include(a => a.Stuffs)
                                        .Include(a => a.Statistic)
                                        .FirstOrDefault(e => e.Email == email);
            if (account is null) throw new Exception("Аккаунт не найден");
            return account;
        }
    }
}