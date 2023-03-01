using LemonDB.Interfaces;

namespace LemonDB.Models;
public abstract class DbEntity : IEntity
{
    public Guid Id { get; set; }
}