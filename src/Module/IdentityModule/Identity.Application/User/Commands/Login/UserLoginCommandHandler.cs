using Framework.Application.Abstraction;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.Login;

public class UserLoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserService userService , 
        IRefreshTokenRepository refreshTokenRepository , 
        IUnitOfWork uow) :
        IRequestHandler<UserLoginCommand, Result<TokenResponseDto>>
{
    public async Task<Result<TokenResponseDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<TokenResponseDto>.Failure("user was not found.");

        var loginResult = await signInManager.PasswordSignInAsync(user, request.Password, true, true);
        if (!loginResult.Succeeded)
        {
            var errorMessage = loginResult.IsLockedOut
                ? "Account is locked out due to multiple failed attempts."
                : "Invalid login credentials.";
            return Result<TokenResponseDto>.Failure(errorMessage);
        }

        var refreshToken = userService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            ExpirationDateTime = DateTime.UtcNow.AddDays(7),
            IsExpired = false,
            IssuedDateTime = DateTime.UtcNow,
            Token = refreshToken , 
            UserId = user.Id
        };

        await refreshTokenRepository.AddAsync(refreshTokenEntity , cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Result<TokenResponseDto>.Success(new TokenResponseDto
        (
            userService.GenerateAccessToken(user),
            refreshToken
        ));
    }
}
