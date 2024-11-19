namespace Framework.Application.Shared.Command;

public interface IDeleteCommand<TKey> : ICommand
{
    public TKey? Id { get; init; }
}