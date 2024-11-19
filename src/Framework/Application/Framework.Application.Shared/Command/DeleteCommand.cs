namespace Framework.Application.Shared.Command;

public record DeleteCommand<TKey> : IDeleteCommand<TKey>
{
    public TKey? Id { get; init; }
}