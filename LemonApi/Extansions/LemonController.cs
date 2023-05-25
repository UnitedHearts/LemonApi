using LemonDB;
using Microsoft.AspNetCore.Authorization;
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
            if (email == null)
                throw new Exception("Невалидный токен");
            var user = _db.Accounts
                            .Include(a => a.Cash)
                            .Include(a => a.Stuffs)
                            .Include(a => a.Statistic)
                            //.Include(a => a.Sessions).ThenInclude(e => e.Map)
                            .FirstOrDefault(e => e.Email == email);
            if (user == null)
                throw new Exception("Аккаунт не найден");
            return user;
        }
    }
}

