using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.Login;

public record UserLoginCommand(
    string Email,
    string Password) :
    IRequest<Result<TokenResponseDto>>;
