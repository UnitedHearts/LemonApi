using LemonApi.Extansions;
using LemonApi.Models;
using LemonDB;
using LemonDB.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class CRUDController : LemonController
{
    public CRUDController(LemonDbContext dbContext) : base(dbContext)
    {
    }

    #region ----------- Map -----------
    [HttpGet("Map/Get/{id}")]
    public async Task<Map> GetMap(Guid id)
    {
        return await _db.Maps.FirstOrDefaultAsync(map => map.Id == id) ?? throw new Exception("Карта не найдена");
    }

    [HttpGet("Map/Get")]
    public async Task<IEnumerable<Map>> GetMap()
    {
        return await _db.Maps.ToListAsync();
    }

    [HttpPost("Map/Create")]
    public async Task<Map> CreateMap(Map map)
    {
        var newMap = new MapBuilder()
                            .SetKey(map.GameKey)
                            .SetName(map.Name)
                            .SetMaxPlayers(map.MaxPlayersCount)
                            .SetDescription(map.Description)
                            .Build();
        _db.Maps.Add(newMap);
        await _db.SaveChangesAsync();
        return newMap;
    }

    [HttpPut("Map/Update")]
    public async Task<Map> UpdateMap(MapInfo mapInfo)
    {
        if (mapInfo.Id == null) throw new Exception("Неизвестная карта для изменения");
        var oldMap = await _db.Maps.FirstOrDefaultAsync(e => e.Id == mapInfo.Id) ?? throw new Exception("Карта не найдена");
        var mapBuilder = new MapBuilder(oldMap)
                            .SetName(mapInfo.Name ?? oldMap.Name)
                            .SetKey(mapInfo.GameKey ?? oldMap.GameKey)
                            .SetDescription(mapInfo.Description ?? oldMap.Description)
                            .SetMaxPlayers(mapInfo.MaxPlayersCount ?? oldMap.MaxPlayersCount);
        await _db.SaveChangesAsync();
        return mapBuilder.Build();
    }

    [HttpDelete("Map/Delete")]
    public async Task DeleteMap(Guid id)
    {
        await _db.Sessions.Where(e => e.Map.Id == id).ForEachAsync(e => e.Map = null);
        _db.Maps.Remove(await _db.GetMapAsync(id));
        await _db.SaveChangesAsync();
    }

    #endregion

    #region ---------- Stuff ----------
    [HttpGet("Stuff/Get/{id}")]
    public async Task<Stuff> GetStuff(Guid id)
    {
        return await _db.Stuffs.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Продукт не найден");
    }

    [HttpGet("Stuff/Get")]
    public async Task<IEnumerable<Stuff>> GetStuff()
    {
        return await _db.Stuffs.ToListAsync();
    }

    [HttpPost("Stuff/Create")]
    public async Task<Stuff> CreateStuff(StuffInfo stuff)
    {
        var newStuff = new StuffBuilder()
                            .SetKey(stuff.GameKey ?? "NoKey")
                            .SetName(stuff.Name ?? "NoName")
                            .SetType(stuff.Type ?? "Default")
                            .SetPrice(stuff.Price ?? 0)
                            .Build();
        _db.Stuffs.Add(newStuff);
        await _db.SaveChangesAsync();
        return newStuff;
    }

    [HttpPut("Stuff/Update")]
    public async Task<Stuff> UpdateStuff(StuffInfo stuffinfo)
    {
        if (stuffinfo.Id == null) throw new Exception("Неизвестный продукт для изменения");
        var oldStuff = await _db.Stuffs.FirstOrDefaultAsync(e => e.Id == stuffinfo.Id) ?? throw new Exception("Продукт не найден");
        var stuffBuilder= new StuffBuilder(oldStuff)
                            .SetKey(stuffinfo.GameKey ?? oldStuff.GameKey)
                            .SetName(stuffinfo.Name ?? oldStuff.Name)
                            .SetType(stuffinfo.Type ?? oldStuff.Type)
                            .SetPrice(stuffinfo?.Price ?? oldStuff.Price);
        await _db.SaveChangesAsync();
        return stuffBuilder.Build();
    }

    [HttpDelete("Stuff/Delete")]
    public async Task DeleteStuff(Guid id)
    {
        var stuff = await _db.Stuffs.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Продукт не найден для удаления");
        _db.Stuffs.Remove(stuff);
        await _db.SaveChangesAsync();
    }
    #endregion
}
