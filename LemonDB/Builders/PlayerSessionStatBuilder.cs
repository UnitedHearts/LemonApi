using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.PlayerSessionStat>;
using ThisBuilder = LemonDB.Builders.PlayerSessionStatBuilder;

namespace LemonDB.Builders;

public class PlayerSessionStatBuilder : InterfaceTemplate
{
    PlayerSessionStat InterfaceTemplate._alias { get; set; }
    PlayerSessionStat Empty => new() { Coins = 0, Rank = 1, DeadTimePoint = 0, Account = null, Fails = 0, Punches = 0, Session = null, SpawnTimePoint = 0 };
    public PlayerSessionStatBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public PlayerSessionStatBuilder(PlayerSessionStat template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }

    public ThisBuilder SetAccount(Account account)
    {
        ((InterfaceTemplate)this)._alias.Account = account;
        return this;
    }
    public ThisBuilder SetSession(Session session)
    {
        ((InterfaceTemplate)this)._alias.Session = session;
        return this;
    }
    public ThisBuilder SetRank(int rank)
    {
        ((InterfaceTemplate)this)._alias.Rank = rank;
        return this;
    }
    public ThisBuilder SetCoins(int coins)
    {
        ((InterfaceTemplate)this)._alias.Coins = coins;
        return this;
    }
    public ThisBuilder SetFails(int fails)
    {
        ((InterfaceTemplate)this)._alias.Fails = fails;
        return this;
    }
    public ThisBuilder SetPunches(int punches)
    {
        ((InterfaceTemplate)this)._alias.Punches = punches;
        return this;
    }
    public ThisBuilder SetSpawnTimePoint(double spawn)
    {
        ((InterfaceTemplate)this)._alias.SpawnTimePoint = spawn;
        return this;
    }
    public ThisBuilder SetDeadTimePoint(double dead)
    {
        ((InterfaceTemplate)this)._alias.DeadTimePoint = dead;
        return this;
    }
    public PlayerSessionStat Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
