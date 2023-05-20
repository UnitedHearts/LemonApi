using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Account>;
using ThisBuilder = LemonDB.Builders.AccountBuilder;

namespace LemonDB.Builders;

public class AccountBuilder : InterfaceTemplate
{
    Account InterfaceTemplate._alias { get; set; }
    Account Empty => new() { EmailConfirmed = false, Active = false };
    public AccountBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public AccountBuilder(Account template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }

    public ThisBuilder SetName(string name)
    {
        ((InterfaceTemplate)this)._alias.Name = name;
        return this;
    }
    public ThisBuilder SetEmail(string email)
    {
        ((InterfaceTemplate)this)._alias.Email = email;
        return this;
    }
    public ThisBuilder SetPassword(string password)
    {
        ((InterfaceTemplate)this)._alias.Password = password;
        return this;
    }
    public ThisBuilder SetState(string state)
    {
        ((InterfaceTemplate)this)._alias.State = state;
        return this;
    }
    public ThisBuilder SetCash(Cash? cash = default)
    {
        ((InterfaceTemplate)this)._alias.Cash = cash ?? new CashBuilder().Build();
        return this;
    }
    public ThisBuilder SetStatistic(AccountStatistic? statistic = default)
    {
        ((InterfaceTemplate)this)._alias.Statistic = statistic ?? new AccountStatisticBuilder().Build();
        return this;
    }
    public ThisBuilder AddStuff(Stuff stuff)
    {
        ((InterfaceTemplate)this)._alias.Stuffs.Add(stuff);
        return this;
    }
    public ThisBuilder AddSessions(IEnumerable<Session> sessions)
    {
        ((InterfaceTemplate)this)._alias.Sessions.ToList().AddRange(sessions);
        return this;
    }
    public ThisBuilder Confirmed()
    {
        ((InterfaceTemplate)this)._alias.EmailConfirmed = !((InterfaceTemplate)this)._alias.EmailConfirmed;
        return this;
    }
    public ThisBuilder Disable()
    {
        ((InterfaceTemplate)this)._alias.Active = false;
        return this;
    }
    public ThisBuilder Active()
    {
        ((InterfaceTemplate)this)._alias.Active = true;
        return this;
    }
    public Account Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}