namespace Framework.Application.Shared.Query;

public abstract class FindQueryHandler<TQuery, TResponse, TAggregate, TKey>(IRepository<TAggregate> repository) :
    IQueryHandler<TQuery, TResponse>
    where TQuery : FindQuery<TResponse, TKey>
    where TAggregate : class
{
    protected readonly IRepository<TAggregate> Repository = repository;

    public virtual async Task<Result<TResponse>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var aggregate = await Repository.FindByIdAsync(request.Id, cancellationToken);

        return aggregate is null
            ? Result<TResponse>.Success(default)
            : await GetResult(aggregate, cancellationToken);
    }

    public abstract Task<Result<TResponse>> GetResult(TAggregate aggregate, CancellationToken cancellationToken);
}