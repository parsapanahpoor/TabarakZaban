using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.SendForgetPasswordEmail;

public record SendForgetPasswordEmailCommand(
    string Email) :
    IRequest<Result<string>>;
