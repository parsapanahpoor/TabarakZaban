using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;
using System.Security.Policy;

namespace Identity.Application.User.Commands.Register;

public class UserRegisterCommandHandler(
    UserManager<ApplicationUser> userManager ) :
    IRequestHandler<UserRegisterCommand, Result>
{
    public async Task<Result> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var applicationUser = new ApplicationUser()
        {
            UserName = request.Email,
            Email = request.Email
        };

        return (await userManager.CreateAsync(applicationUser, request.Password)) 
            is var res && res.Succeeded
            ? Result.Success
            : Result.Failure(new Error(
                code: string.Join(",", res.Errors.Select(p => p.Code)),
                message: string.Join(",", res.Errors.Select(p => p.Description))
            ));
    }
}