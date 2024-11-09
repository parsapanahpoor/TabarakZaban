using System.Collections.ObjectModel;

namespace Framework.Domain.Shared;

public abstract class Aggregate<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    where TKey : notnull
{
    protected Aggregate()
    {

    }
    protected Aggregate(TKey id) : base(id)
    {
    }
}