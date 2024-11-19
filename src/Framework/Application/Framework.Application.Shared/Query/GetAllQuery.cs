using Framework.Application.Shared.Models;

namespace Framework.Application.Shared.Query;

public record GetAllQuery<TResult> : QueryParameter, IQuery<DataResult<TResult>>
    where TResult : class;