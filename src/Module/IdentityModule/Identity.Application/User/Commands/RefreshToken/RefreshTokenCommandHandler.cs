using Framework.Application.Abstraction;
using Identity.Domain;
using Identity.Domain.Entities;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUserService userService , 
    UserManager<ApplicationUser> userManager , 
    IRefreshTokenRepository refreshTokenRepository , 
    IUnitOfWork uow) :
    IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await refreshTokenRepository
            .FirstOrDefaultAsync(p=> p.Token.Equals(request.RefreshToken) , cancellationToken);
        if (storedToken == null || storedToken.IsExpired)
            return Result<TokenResponseDto>.Failure("Invalid or expired refresh token.");

        var user = await userManager.FindByIdAsync(storedToken.UserId.ToString()!);
        if (user == null)
            return Result<TokenResponseDto>.Failure("User not found.");

        var newAccessToken = userService.GenerateAccessToken(user);
        var newRefreshToken = userService.GenerateRefreshToken();

        storedToken.Token = newRefreshToken;
        storedToken.ExpirationDateTime = DateTime.UtcNow.AddDays(7); 

        refreshTokenRepository.Update(storedToken);
        await uow.SaveChangesAsync(cancellationToken);

        return Result<TokenResponseDto>.Success(new TokenResponseDto
        (
             newAccessToken,
            newRefreshToken
        ));
    }
}
