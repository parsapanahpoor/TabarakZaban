namespace Framework.Application.Abstraction.Messaging;

public interface ICacheableQuery : ICommand;

public interface ICacheableQuery<TResponse> : ICommand<TResponse>, ICacheableQuery;