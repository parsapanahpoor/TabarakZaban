using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
   string UserId,
   string Tokken) :
   IRequest<Result>;
