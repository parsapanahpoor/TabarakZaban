namespace Framework.Application.Abstraction.Messaging;

public interface ITransactionalCommand : ICommand;

public interface ITransactionalCommand<TResponse> : ICommand<TResponse>, ITransactionalCommand;