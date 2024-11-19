using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.RefreshToken;

public record RefreshTokenCommand(
    string RefreshToken) : 
    IRequest<Result<TokenResponseDto>>;
