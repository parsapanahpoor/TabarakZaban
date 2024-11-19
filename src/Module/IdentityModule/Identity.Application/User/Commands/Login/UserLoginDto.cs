namespace Identity.Application.User.Commands.Login;

public record UserLoginDto(
    string Email ,
    string Password);
