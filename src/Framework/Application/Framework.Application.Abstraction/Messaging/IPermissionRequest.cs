namespace Framework.Application.Abstraction.Messaging;

public interface IPermissionRequest : ICommand;

public interface IPermissionRequest<TResponse> : ICommand<TResponse>, IPermissionRequest;