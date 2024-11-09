using AutoMapper;
using FluentValidation;

namespace Framework.Domain.Shared;

public class SimpleBaseService<TAggregate, TModel>(IMapper mapper, IValidator<TAggregate> validator) :
    SimpleBaseService<TAggregate, TModel, TModel>(mapper, validator),
    ISimpleBaseService<TAggregate, TModel>
    where TAggregate : IAggregateRoot
    where TModel : class
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TAggregate> _validator = validator;

    public override async Task CreateEvaluation(TAggregate originalAggregate, TModel data, CancellationToken cancellationToken)
    {
        await Evaluation(originalAggregate, data, cancellationToken);
    }

    public override async Task UpdateEvaluation(TAggregate originalAggregate, TModel data, CancellationToken cancellationToken)
    {
        await Evaluation(originalAggregate, data, cancellationToken);
    }

    public virtual Task Evaluation(TAggregate originalAggregate,
        TModel data,
        CancellationToken cancellationToken)
        => Task.CompletedTask;
}

public class SimpleBaseService<TAggregate, TCreateModel, TUpdateModel>(IMapper mapper, IValidator<TAggregate> validator) :
    ISimpleBaseService<TAggregate, TCreateModel, TUpdateModel>
    where TAggregate : IAggregateRoot
    where TCreateModel : class
    where TUpdateModel : class
{
    public virtual async Task<Result<TAggregate>> Create(TCreateModel data, CancellationToken cancellationToken)
    {
        var aggregate = mapper.Map<TAggregate>(data);

        await CreateEvaluation(aggregate, data, cancellationToken);

        if (await ValidateAggregate(aggregate, cancellationToken) is {IsFailure: true} validateResult)
            return validateResult;

        return Result<TAggregate>.Success(aggregate);
    }

    public virtual async Task<Result<TAggregate>> Update(TAggregate originalAggregate, TUpdateModel data, CancellationToken cancellationToken)
    {
        var aggregate = mapper.Map(data, originalAggregate);

        await UpdateEvaluation(aggregate, data, cancellationToken);

        if (await ValidateAggregate(aggregate, cancellationToken) is {IsFailure: true} validateResult)
            return validateResult;

        return Result<TAggregate>.Success(originalAggregate);
    }

    public virtual Task CreateEvaluation(TAggregate originalAggregate, TCreateModel data, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public virtual Task UpdateEvaluation(TAggregate originalAggregate, TUpdateModel data, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public virtual async Task<Result<TAggregate>> ValidateAggregate(TAggregate aggregate, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(aggregate, cancellationToken);
        if (!validationResult.IsValid)
            return Result<TAggregate>.Failure().FromValidation(validationResult);

        return Result<TAggregate>.Success(aggregate);
    }
}