namespace Framework.Presentation.Shared;

public static class CreateEndpoints
{
    public static RouteHandlerBuilder MapCreate<TCommand>(this IEndpointRouteBuilder endpoints,
        string? pattern = null)
        where TCommand : ICreateCommand
    {
        pattern ??= "/Create";
        var result = endpoints.MapPost(pattern,
                async Task<IResult> (TCommand request,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(request, cancellationToken);
                    return result.IsSuccess
                        ? TypedResults.Created()
                        : TypedResults.BadRequest(result.Errors);
                })
            .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Create for {typeof(TCommand).Name}";
                generatedOperation.Summary = $"Create for {typeof(TCommand).Name}"; 
                return generatedOperation;
            }) 
            .Produces((int) HttpStatusCode.Created)
            .Produces((int) HttpStatusCode.BadRequest);
        return result;
    }
}