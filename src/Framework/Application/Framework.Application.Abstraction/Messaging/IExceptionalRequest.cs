namespace Framework.Application.Abstraction.Messaging;

public interface IExceptionalRequest : ICommand;

public interface IExceptionalRequest<TResponse> : ICommand<TResponse>, IExceptionalRequest;