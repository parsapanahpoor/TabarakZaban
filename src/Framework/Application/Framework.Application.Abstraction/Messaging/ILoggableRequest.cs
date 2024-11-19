namespace Framework.Application.Abstraction.Messaging;

public interface ILoggableRequest : ICommand;

public interface ILoggableRequest<TResponse> : ICommand<TResponse>, ILoggableRequest;