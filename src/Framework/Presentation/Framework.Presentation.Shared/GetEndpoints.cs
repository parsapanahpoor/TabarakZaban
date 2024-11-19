namespace Framework.Presentation.Shared;

public static class GetEndpoints
{
    public static RouteHandlerBuilder MapGetOne<TQuery, TResult, TKey>(this IEndpointRouteBuilder endpoints,
        string? pattern = null)
        where TQuery : FindQuery<TResult, TKey>, new()
    {
        pattern ??= "/GetOne";
        pattern += "/{id}";
        var result = endpoints.MapGet(pattern,
                async Task<IResult> (TKey id,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var query = new TQuery()
                    {
                        Id = id
                    };
                    var result = await mediator.Send(query, cancellationToken);
                    return result.IsSuccess
                        ? result.Value is not null ? TypedResults.Ok(result.Value) : TypedResults.NotFound()
                        : TypedResults.BadRequest(result.Errors);
                })
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Get for {typeof(TQuery).Name}";
                generatedOperation.Summary = $"Get for {typeof(TQuery).Name}";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest);
        return result;
    }
}