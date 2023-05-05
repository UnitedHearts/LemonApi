using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.AccountPlayStatistic>;
using ThisBuilder = LemonDB.Builders.AccountStatisticBuilder;

namespace LemonDB.Builders;

public class AccountStatisticBuilder : InterfaceTemplate
{
    AccountPlayStatistic InterfaceTemplate._alias { get; set; }
    public AccountStatisticBuilder()
    {
        ((InterfaceTemplate)this)._alias = new() { Deaths = 0, Money = 0, TotalMoney = 0, Wins = 0 };
    }
    public AccountStatisticBuilder(AccountPlayStatistic template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }
    public AccountStatisticBuilder(Account account)
    {
        ((InterfaceTemplate)this)._alias = new() { Account = account, Deaths = 0, Money = 0, TotalMoney = 0, Wins = 0 };
    }

    public ThisBuilder SetAccount(Account account)
    {
        ((InterfaceTemplate)this)._alias.Account = account;
        return this;
    }
    public ThisBuilder SetMoney(double money)
    {
        ((InterfaceTemplate)this)._alias.Money = money;
        return this;
    }
    public ThisBuilder SetTotalMoney(int totalMoney)
    {
        ((InterfaceTemplate)this)._alias.TotalMoney = totalMoney;
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

    public AccountPlayStatistic Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = new();
        return alias;
    }
}
