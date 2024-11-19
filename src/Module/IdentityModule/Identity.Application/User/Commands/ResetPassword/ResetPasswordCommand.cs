using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.ResetPassword;

public record ResetPasswordCommand(
    string UserId,
    string Token,
    string Password,
    string ConfirmPassword ) :
    IRequest<Result>;

