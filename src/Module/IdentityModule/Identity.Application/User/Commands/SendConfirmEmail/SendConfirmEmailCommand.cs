using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.SendConfirmEmail;

public record SendConfirmEmailCommand(
    string Email) :
    IRequest<Result<string>>;
