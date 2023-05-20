using LemonDB.Interfaces;

namespace LemonDB;

public class Map : DbEntity, IKey
{
    public string Name { get; set; }
    public string GameKey { get; set; }
    public string Description { get; set; }
    public int MaxPlayersCount { get; set; }
}
