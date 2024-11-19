namespace Framework.Presentation.Shared;

public static class UpdateEndpoints
{
    public static RouteHandlerBuilder MapUpdate<TCommand, TKey>(this IEndpointRouteBuilder endpoints,
        string? pattern = null)
        where TCommand : IUpdateCommand<TKey>, new()
    {
        pattern ??= "/Update";
        pattern += "/{id}";
        var result = endpoints.MapPut(pattern,
                async Task<IResult> (TKey id, TCommand request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    request.Id = id;
                    var result = await mediator.Send(request, cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Update for {typeof(TCommand).Name}";
                generatedOperation.Summary = $"Update for {typeof(TCommand).Name}";
                return generatedOperation;
            })
            .Produces((int) HttpStatusCode.OK)
            .Produces((int) HttpStatusCode.NotFound)
            .Produces((int) HttpStatusCode.BadRequest);
        return result;
    }
}