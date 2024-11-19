namespace Framework.Presentation.Shared;

public static class GetAllEndpoints
{
    public static RouteHandlerBuilder MapGetAll<TQuery,TResult>(this IEndpointRouteBuilder endpoints,
        string? pattern = null)
        where TQuery : GetAllQuery<TResult>
        where TResult : class
    {
        pattern ??= "/GetAll";
        var result = endpoints.MapPost(pattern,
                async Task<IResult> (TQuery request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                 {
                    var result = await mediator.Send(request, cancellationToken);
                    return result.IsSuccess
                        ? TypedResults.Ok(result.Value)
                        : TypedResults.BadRequest(result.Errors);
                })
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"GetAll for {typeof(TQuery).Name}";
                generatedOperation.Summary = $"GetAll for {typeof(TQuery).Name}";
                return generatedOperation;
            })
            .Produces((int) HttpStatusCode.OK)
            .Produces((int) HttpStatusCode.BadRequest);
        return result;
    }
}