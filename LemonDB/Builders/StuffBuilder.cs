using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Stuff>;
using ThisBuilder = LemonDB.Builders.StuffBuilder;

namespace LemonDB.Builders;

public class StuffBuilder : InterfaceTemplate
{
    Stuff InterfaceTemplate._alias { get; set; }
    Stuff Empty => new() { Name = "", GameKey = "", Price = 0, Type = "", Accounts = new List<Account>() };
    public StuffBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public StuffBuilder(Stuff template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }

    public ThisBuilder SetName(string name)
    {
        ((InterfaceTemplate)this)._alias.Name = name;
        return this;
    }
    public ThisBuilder SetType(string type)
    {
        ((InterfaceTemplate)this)._alias.Type = type;
        return this;
    }
    public ThisBuilder SetPrice(double price)
    {
        ((InterfaceTemplate)this)._alias.Price = price;
        return this;
    }
    public ThisBuilder SetKey(string key)
    {
        ((InterfaceTemplate)this)._alias.GameKey = key;
        return this;
    }
    public Stuff Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
