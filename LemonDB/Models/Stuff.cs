using LemonDB.Interfaces;

namespace LemonDB;

public class Stuff : DbEntity, IKey
{
    public string Name { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }
    public string GameKey { get; set; }

    public IEnumerable<Account> Accounts{ get; set; }
}
