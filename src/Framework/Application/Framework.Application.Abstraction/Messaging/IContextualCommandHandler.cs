namespace Framework.Application.Abstraction.Messaging;

public interface IContextualCommandHandler<in TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : IContextualRequest;

public interface IContextualCommandHandler<in TCommand, TData>
    : IRequestHandler<TCommand, Result>
    where TCommand : IContextualRequest<TData>
    where TData : class;

public interface IContextualCommandHandler<in TCommand, TData, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : IContextualRequest<TData, TResponse>
    where TData : class;