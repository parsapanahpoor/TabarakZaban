using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.ResetPassword;

public class ResetPasswordCommandHandler(
    UserManager<ApplicationUser> userManager) :
    IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Failure("user was not found.");

        return (await userManager.ResetPasswordAsync(user, request.Token, request.Password))
         is var res && res.Succeeded
         ? Result.Success
         : Result.Failure(new Error(
             code: string.Join(",", res.Errors.Select(p => p.Code)),
             message: string.Join(",", res.Errors.Select(p => p.Description))
         ));
    }
}

