namespace Identity.Application.User.Commands.Register;

public record UserRegisterDto(
    string Email,
    string Password , 
    string ConfirmPassword );
