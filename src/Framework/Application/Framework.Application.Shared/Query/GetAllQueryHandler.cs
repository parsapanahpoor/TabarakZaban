using Framework.Domain.Abstraction.ReadParameters;

namespace Framework.Application.Shared.Query;

public abstract class GetAllQueryHandler<TAggregate, TQuery, TResult>(IRepository<TAggregate> repository) :
    IQueryHandler<TQuery, DataResult<TResult>>
    where TAggregate : class
    where TQuery : IQuery<DataResult<TResult>>, IQueryParameter
    where TResult : class
{
    protected readonly IRepository<TAggregate> Repository = repository;

    public virtual async Task<Result<DataResult<TResult>>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var data = await Repository.GetAllAsync<TResult>(request, cancellationToken);
        var total = await Repository.GetTotalAsync(request, cancellationToken);
 
        return Result<DataResult<TResult>>.Success(new DataResult<TResult>()
        {
            Data = data,
            Total = total
        });
    }
}