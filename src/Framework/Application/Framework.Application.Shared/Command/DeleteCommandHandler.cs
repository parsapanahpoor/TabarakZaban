using Framework.Application.Shared.GlobalErrors;

namespace Framework.Application.Shared.Command;

public abstract class DeleteCommandHandler<TCommand, TAggregate, TKey>(
    IUnitOfWork uow,
    IRepository<TAggregate> repository)
    : ICommandHandler<TCommand>
    where TCommand : DeleteCommand<TKey>
    where TAggregate : class
{
    public async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var item = await repository.FindByIdAsync(request.Id, cancellationToken);
        if (item is null)
            return Result.Failure(new ItemNotFoundError());

        repository.Remove(item);
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}