namespace Framework.Application.Shared.Command;

public abstract class CreateCommandHandler<TCommand, TAggregate>(
    IUnitOfWork uow,
    IRepository<TAggregate> repository)
    : ICommandHandler<TCommand>
    where TCommand : ICreateCommand
    where TAggregate : class
{
    public virtual async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var domainResult = await CreateAggregate(request, cancellationToken);
        if (domainResult.IsFailure)
            return Result.Failure(domainResult.Errors ?? []);

        await repository.AddAsync(domainResult.Value!, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }

    public abstract Task<Result<TAggregate>> CreateAggregate(TCommand request, CancellationToken cancellationToken);
}