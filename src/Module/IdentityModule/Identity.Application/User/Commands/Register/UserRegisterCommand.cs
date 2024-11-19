using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.Register;

public record UserRegisterCommand(
    string Email,
    string Password ,
    string ConfirmPassword ) :
    IRequest<Result>;
