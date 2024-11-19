using Identity.Application.User.Commands.Login;

namespace Identity.Application.User.Commands.RefreshToken;

public record RefreshTokenDto(
    string RefreshToken);
