namespace Identity.Application.User.Commands;

public record TokenResponseDto(
    string AccessToken,
    string RefreshToken);
