using FluentValidation;
using MediatR;

namespace Framework.Application.Shared.Behavior
{
    public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<Result>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any()) 
                return await next(); 

            var context = new ValidationContext<TRequest>(request); 
            var errors = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Select(p => new Error(p.ErrorCode, p.ErrorMessage))
                .DistinctBy(p => p.Message).ToList();

            return errors.Any() ? (TResponse) Result.Failure(errors) : await next();
        }
    }
}