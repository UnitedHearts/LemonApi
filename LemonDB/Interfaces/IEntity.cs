namespace LemonDB.Interfaces;

public interface IEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid Id { get; set; }
}