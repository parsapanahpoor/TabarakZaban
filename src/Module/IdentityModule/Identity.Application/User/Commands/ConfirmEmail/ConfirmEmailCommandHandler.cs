using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(
    UserManager<ApplicationUser> userManager) :
    IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) 
            return Result.Failure("user was not found.");

        return (await userManager.ConfirmEmailAsync(user, request.Tokken))
        is var res && res.Succeeded
        ? Result.Success
        : Result.Failure("wrong informations.");
    }
}