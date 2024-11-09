namespace Framework.Domain.Abstraction;

public interface IEntity<out TKey> : IDeleteAble
{
    TKey Id { get; }  
}