namespace Framework.Presentation.Shared;

public static class DeleteEndpoints
{
    public static RouteHandlerBuilder MapDelete<TCommand, TKey>(this IEndpointRouteBuilder endpoints,
        string? pattern = null)
        where TCommand : DeleteCommand<TKey>, new()
    {
        pattern ??= "/Delete";
        pattern += "/{id}";
        var result = endpoints.MapDelete(pattern,
                async Task<IResult> (TKey id,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var command = new TCommand()
                    {
                        Id = id
                    };
                    var result = await mediator.Send(command, cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Delete for {typeof(TCommand).Name}";
                generatedOperation.Summary = $"Delete for {typeof(TCommand).Name}";
                return generatedOperation;
            })
            .Produces((int) HttpStatusCode.OK)
            .Produces((int) HttpStatusCode.NotFound)
            .Produces((int) HttpStatusCode.BadRequest);
        return result;
    }
}