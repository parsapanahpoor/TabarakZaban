using System.Collections.ObjectModel;

namespace Framework.Domain.Abstraction;

public interface IAggregateRoot<out TKey> : IEntity<TKey>;

public interface IAggregateRoot;