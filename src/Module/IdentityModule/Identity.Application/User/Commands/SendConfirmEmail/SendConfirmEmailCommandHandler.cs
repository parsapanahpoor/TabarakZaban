using Identity.Application.Services;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedProject;
using SharedProject.Extensions;

namespace Identity.Application.User.Commands.SendConfirmEmail;

public class SendConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager,
    IMessageService messageService ,
    IOptions<ApplicationSettings> options) :
    IRequestHandler<SendConfirmEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result<string>.Failure("user was not found.");

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var linkGenerator = new LinkGeneratorService(options);
        string confirmationLink = linkGenerator.GenerateConfirmEmailLink("Identity" , "ConfirmEmail", user.Id, token);

        //await messageService.SendActivationLink(
        //    "info@.com",
        //    user.Email,
        //    "Confirm your email address",
        //    confirmationLink);

        return Result<string>.Success(confirmationLink);
    }
}