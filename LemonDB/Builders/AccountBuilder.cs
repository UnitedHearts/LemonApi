using LemonDB.Models;
using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Models.Account>;
using ThisBuilder = LemonDB.Builders.AccountBuilder;

namespace LemonDB.Builders;

public class AccountBuilder : InterfaceTemplate
{
    Account InterfaceTemplate._alias { get; set; }
    public AccountBuilder()
    {
        ((InterfaceTemplate)this)._alias = new() { EmailConfirmed = false, Active = false };
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
        ((InterfaceTemplate)this)._alias = new() { EmailConfirmed = false, Active = false };
        return alias;
    }
}