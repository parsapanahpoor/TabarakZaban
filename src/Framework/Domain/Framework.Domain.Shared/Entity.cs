using SharedProject.Security;

namespace Framework.Domain.Shared;

public abstract class Entity<TKey> : IEquatable<Entity<TKey>>, IEntity<TKey>
    where TKey : notnull
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity()
    {
    }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    protected Entity(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; set; }
    public bool IsDelete { get; set; } = false;

    public override bool Equals(object? obj)
    {
        if (obj is null ||
            obj.GetType() != GetType() ||
            obj is not Entity<TKey> entity)
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null ||
            other.GetType() != GetType())
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TKey>? first, Entity<TKey>? second) => first is not null && first.Equals(second);
    public static bool operator !=(Entity<TKey>? first, Entity<TKey>? second) => !(first == second);

    public override int GetHashCode() => SecurityUtils.HashCodeSalter(Id.GetHashCode());
}