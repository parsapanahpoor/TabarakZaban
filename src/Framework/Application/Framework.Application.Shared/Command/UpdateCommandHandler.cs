using Framework.Application.Shared.GlobalErrors;

namespace Framework.Application.Shared.Command;

public abstract class UpdateCommandHandler<TCommand, TAggregate, TKey>(
    IUnitOfWork uow,
    IRepository<TAggregate> repository)
    : ICommandHandler<TCommand>
    where TCommand : IUpdateCommand<TKey>
    where TAggregate : class
{
    public async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var item = await repository.FindByIdAsync(request.Id, cancellationToken);
        if (item is null)
            return Result.Failure(new ItemNotFoundError());
        
        uow.Attach(item);
        var domainResult = await UpdateAggregate(item, request, cancellationToken);
        if (domainResult.IsFailure)
            return Result.Failure(domainResult.Errors ?? []);

        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }

    protected abstract Task<Result> UpdateAggregate(TAggregate aggregate, TCommand request, CancellationToken cancellationToken);
}