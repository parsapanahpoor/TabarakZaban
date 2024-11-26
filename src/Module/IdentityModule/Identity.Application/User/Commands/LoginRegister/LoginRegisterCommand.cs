using MediatR;
using SharedProject;

namespace Identity.Application.User.Commands.LoginRegister;

public record LoginRegisterCommand(
	LoginRegisterDto data) :
	IRequest<Result>;
