using Contracts.Http;
using LemonDB;
using LemonDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace LemonApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController
{
    readonly LemonDbContext _db;

    public AuthorizationController(LemonDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<Answer<Account>> Login(LoginModel login)
    {
        login.Validate();
        var acc = _db.Accounts.FirstOrDefault(e => e.Email == login.Email && e.Password == login.Password);
        if (acc == null)
            throw new Exception("Неврные логин или пароль");
        return new(acc);
    }
}
