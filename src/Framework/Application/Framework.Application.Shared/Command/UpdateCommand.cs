namespace Framework.Application.Shared.Command;

public record UpdateCommand<TKey>: IUpdateCommand<TKey>
{
    public TKey Id { get; set; } = default!;
}