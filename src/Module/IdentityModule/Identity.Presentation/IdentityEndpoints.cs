using Identity.Application.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Framework.Presentation.Shared;
using SharedProject;
using System.Net;
using Framework.Application.Shared.GlobalErrors;
using Identity.Application.User.Commands.Login;
using Identity.Application.User.Commands.ConfirmEmail;
using Identity.Application.User.Commands.SendConfirmEmail;
using Identity.Application.User.Commands.SendForgetPasswordEmail;
using Identity.Application.User.Commands.ResetPassword;
using Microsoft.AspNetCore.SignalR;
using Identity.Application.User.Commands.RefreshToken;


namespace Identity.Presentation;

public class IdentityEndpoints() : StandardBaseEndpoint("Identity", policyNames: Constants.AdminPolicy)
{
    protected override List<RouteHandlerBuilder> GetEndpoint(IEndpointRouteBuilder app) =>
    [
        app.MapPost("/Register",
                async Task<IResult> (
                    UserRegisterDto data,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new UserRegisterCommand(
                        Email : data.Email,
                        Password : data.Password , 
                        ConfirmPassword : data.ConfirmPassword
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"User Registration api";
                generatedOperation.Summary = $"User Registration api";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

        app.MapPost("/Login",
                async Task<IResult> (
                    UserLoginDto data,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new UserLoginCommand(
                        Email : data.Email , 
                        Password : data.Password
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok(result.Value);

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"User Login api";
                generatedOperation.Summary = $"User Login api";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

          app.MapPost("/RefreshToken",
                async Task<IResult> (
                    RefreshTokenDto data,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new RefreshTokenCommand(
                        RefreshToken : data.RefreshToken), 
                        cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok(result.Value);

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"RefreshToken api";
                generatedOperation.Summary = $"RefreshToken api";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

          app.MapPost("/SendActivationEmail",
                async Task<IResult> (
                    string email,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new SendConfirmEmailCommand(
                        Email : email
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Send Activation email";
                generatedOperation.Summary = $"Send Activation email";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

        app.MapGet("/ConfirmEmail/{userId}/{tokken}",
                async Task<IResult> (
                    string userId,
                    string tokken ,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new ConfirmEmailCommand(
                        UserId : userId,
                        Tokken : tokken
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Email Confirm api";
                generatedOperation.Summary = $"User Confirm api";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

          app.MapPost("/ForgetPasswordByEmail",
                async Task<IResult> (
                    string email,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new SendForgetPasswordEmailCommand(
                        email
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Send Email for forget password.";
                generatedOperation.Summary = $"Send Email for forget password.";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),

        app.MapPost("/ResetPassword",
                async Task<IResult> (
                    ResetPasswordDto data,
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken = default) =>
                {
                    var result = await mediator.Send(new ResetPasswordCommand(
                        UserId : data.UserId , 
                        Token : data.Token , 
                        Password : data.Password ,
                        ConfirmPassword : data.ConfirmPassword
                        ), cancellationToken);

                    if (result.IsSuccess)
                        return TypedResults.Ok();

                    if (result.HasError<ItemNotFoundError>())
                        return TypedResults.NotFound();

                    return TypedResults.BadRequest(result.Errors);
                })
        .WithOpenApi(generatedOperation =>
            {
                generatedOperation.Description = $"Reset User Password.";
                generatedOperation.Summary = $"Reset User Password.";
                return generatedOperation;
            })
            .Produces((int)HttpStatusCode.OK)
            .Produces((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.BadRequest),
    ];
}