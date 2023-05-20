using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.AccountStatistic>;
using ThisBuilder = LemonDB.Builders.AccountStatisticBuilder;

namespace LemonDB.Builders;

public class AccountStatisticBuilder : InterfaceTemplate
{
    AccountStatistic InterfaceTemplate._alias { get; set; }
    AccountStatistic Empty => new() { Deaths = 0, Wins = 0, Plays = 0 };
    public AccountStatisticBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public AccountStatisticBuilder(AccountStatistic template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }
    public AccountStatisticBuilder(Account account)
    {
        ((InterfaceTemplate)this)._alias = new() { AccountId = account.Id, Deaths = 0, Wins = 0, Plays = 0 };
    }

    public ThisBuilder SetAccount(Account account)
    {
        ((InterfaceTemplate)this)._alias.AccountId = account.Id;
        return this;
    }
    public ThisBuilder SetDeaths(int deaths)
    {
        ((InterfaceTemplate)this)._alias.Deaths = deaths;
        return this;
    }
    public ThisBuilder SetWins(int wins)
    {
        ((InterfaceTemplate)this)._alias.Wins = wins;
        return this;
    }
    public ThisBuilder SetPlays(int plays)
    {
        ((InterfaceTemplate)this)._alias.Plays = plays;
        return this;
    }
    public ThisBuilder AddDeaths(int deaths)
    {
        ((InterfaceTemplate)this)._alias.Deaths += deaths;
        return this;
    }
    public ThisBuilder AddWins(int wins)
    {
        ((InterfaceTemplate)this)._alias.Wins += wins;
        return this;
    }
    public ThisBuilder AddPlays(int plays)
    {
        ((InterfaceTemplate)this)._alias.Plays += plays;
        return this;
    }
    public AccountStatistic Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
