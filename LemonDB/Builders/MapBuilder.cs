using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Map>;
using ThisBuilder = LemonDB.Builders.MapBuilder;

namespace LemonDB.Builders;

public class MapBuilder : InterfaceTemplate
{
    Map InterfaceTemplate._alias { get; set; }
    Map Empty => new() { Description = "", GameKey = "", Name = "", MaxPlayersCount = 0 };
    public MapBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public MapBuilder(Map template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }
    public ThisBuilder SetName(string name)
    {
        ((InterfaceTemplate)this)._alias.Name = name;
        return this;
    }
    public ThisBuilder SetDescription(string description)
    {
        ((InterfaceTemplate)this)._alias.Description = description;
        return this;
    }
    public ThisBuilder SetKey(string key)
    {
        ((InterfaceTemplate)this)._alias.GameKey= key;
        return this;
    }
    public ThisBuilder SetMaxPlayers(int maxPlayers)
    {
        ((InterfaceTemplate)this)._alias.MaxPlayersCount = maxPlayers;
        return this;
    }
    public Map Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
