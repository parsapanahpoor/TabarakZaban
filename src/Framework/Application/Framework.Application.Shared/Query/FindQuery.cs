#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Framework.Application.Shared.Query;

public record FindQuery<TResult,TKey> : IQuery<TResult>
{
    public TKey Id { get; set; }
}
 