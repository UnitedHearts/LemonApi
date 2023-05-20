using InterfaceTemplate = LemonDB.Interfaces.IDbEntityBuilder<LemonDB.Cash>;
using ThisBuilder = LemonDB.Builders.CashBuilder;

namespace LemonDB.Builders;

public class CashBuilder : InterfaceTemplate
{
    Cash InterfaceTemplate._alias { get; set; }
    Cash Empty => new() { Current = 0, TotalHistory = 0 };
    public CashBuilder()
    {
        ((InterfaceTemplate)this)._alias = Empty;
    }
    public CashBuilder(Cash template)
    {
        ((InterfaceTemplate)this)._alias = template;
    }
    public ThisBuilder SetCash(double money)
    {
        ((InterfaceTemplate)this)._alias.TotalHistory = ((InterfaceTemplate)this)._alias.TotalHistory + (money - ((InterfaceTemplate)this)._alias.Current);
        ((InterfaceTemplate)this)._alias.Current = money;
        return this;
    }
    public ThisBuilder AddCash(double money)
    {
        ((InterfaceTemplate)this)._alias.Current += money;
        if(money > 0)
            ((InterfaceTemplate)this)._alias.TotalHistory += money;
        return this;
    }
    public Cash Build()
    {
        var alias = ((InterfaceTemplate)this)._alias;
        ((InterfaceTemplate)this)._alias = Empty;
        return alias;
    }
}
