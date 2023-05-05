using LemonDB.Interfaces;

namespace LemonDB;
public abstract class DbEntity : IEntity
{
    public Guid Id { get; set; }
}