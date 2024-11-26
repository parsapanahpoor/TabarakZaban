namespace Identity.Application.User.Commands.LoginRegister;

public record LoginRegisterDto(
	string Mobile,
	string? ReturnUrl);
