namespace Contracts;
public interface IBuilder<T> where T : class
{
    ///<summary>
    ///Создает экземпляр целевого обьекта из обьекта конструктора
    ///</summary>
    ///<returns></returns>
    public T Build();
}