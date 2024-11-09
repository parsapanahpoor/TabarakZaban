namespace Framework.Domain.Shared;

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    where TKey : notnull
{
    protected AggregateRoot() 
    {
        
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }
}
