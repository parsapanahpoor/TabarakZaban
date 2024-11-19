namespace Framework.Application.Shared.Command;

public interface IUpdateCommand<TKey> : ICommand
{
    public TKey Id { get; set; }
}