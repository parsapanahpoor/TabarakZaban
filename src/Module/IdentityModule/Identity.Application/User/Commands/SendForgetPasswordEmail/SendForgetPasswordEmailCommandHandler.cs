using Identity.Application.Services;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedProject;
using SharedProject.Extensions;

namespace Identity.Application.User.Commands.SendForgetPasswordEmail;

public class SendForgetPasswordEmailCommandHandler(
    UserManager<ApplicationUser> userManager,
    IMessageService messageService ,
    IOptions<ApplicationSettings> options) :
    IRequestHandler<SendForgetPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendForgetPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null ||
            ! await userManager.IsEmailConfirmedAsync(user))
            return Result<string>.Failure("user was not found or maybe your email didnt confiem.");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var linkGenerator = new LinkGeneratorService(options);
        string confirmationLink = linkGenerator.GenerateConfirmEmailLink("Identity" , "ResetPassword", user.Id, token);

        //await messageService.SendActivationLink(
        //    "info@.com",
        //    user.Email,
        //    "فراموشی رمز عبور",
        //    $"برای تنظیم مجدد کلمه عبور بر روی لینک زیر کلیک کنید <br/> <a href={confirmationLink}> link reset Password </a>");

        return Result<string>.Success(confirmationLink);
    }
}