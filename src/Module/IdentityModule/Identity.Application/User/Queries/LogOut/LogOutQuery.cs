using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Queries.LogOut;

public record LogOutQuery :
    IRequest<Result>;

public record LogOutQueryHandler(
    SignInManager<ApplicationUser>  signInManager) :
    IRequestHandler<LogOutQuery, Result>
{
    public async Task<Result> Handle(LogOutQuery request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Result.Success;
    }
}