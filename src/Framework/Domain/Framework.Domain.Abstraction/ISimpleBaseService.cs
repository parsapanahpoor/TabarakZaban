using SharedProject;

namespace Framework.Domain.Abstraction;

public interface ISimpleBaseService<TAggregate, in TModel>
    where TAggregate : IAggregateRoot
    where TModel : class 
{
    Task<Result<TAggregate>> Create(TModel data, CancellationToken cancellationToken); 
    Task<Result<TAggregate>> Update(TAggregate aggregate, TModel data, CancellationToken cancellationToken); 
    Task Evaluation(TAggregate originalAggregate, TModel data, CancellationToken cancellationToken);
}

public interface ISimpleBaseService<TAggregate, in TCreateModel, in TUpdateModel>
    where TAggregate : IAggregateRoot
    where TCreateModel : class
    where TUpdateModel : class 
{
    Task<Result<TAggregate>> Create(TCreateModel data, CancellationToken cancellationToken); 
    Task<Result<TAggregate>> Update(TAggregate aggregate, TUpdateModel data, CancellationToken cancellationToken); 
    Task CreateEvaluation(TAggregate originalAggregate, TCreateModel data, CancellationToken cancellationToken); 
    Task UpdateEvaluation(TAggregate originalAggregate, TUpdateModel data, CancellationToken cancellationToken); 
    Task<Result<TAggregate>> ValidateAggregate(TAggregate aggregate, CancellationToken cancellationToken);
}