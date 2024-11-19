namespace Framework.Application.Abstraction.Messaging;

public interface IContextualRequest : IRequest<Result>
{
    public Dictionary<string, object> Context { get; set; }
}

public interface IContextualRequest<TData> : IRequest<Result>
    where TData : class
{
    public TData Data { get; init; }
    public Dictionary<string, object> Context { get; set; }
}

public interface IContextualRequest<TData, TResponse> : IRequest<Result<TResponse>>
    where TData : class
{
    public TData Data { get; init; }
    public Dictionary<string, object> Context { get; set; }
}