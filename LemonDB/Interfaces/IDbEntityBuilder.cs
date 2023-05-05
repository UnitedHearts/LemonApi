using Contracts;
using LemonDB;

namespace LemonDB.Interfaces;
public interface IDbEntityBuilder<T> : IBuilder<T> where T : DbEntity
{
    T _alias { get; set; }
}