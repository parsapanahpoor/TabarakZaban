using Framework.Application.Abstraction;
using Identity.Domain.Entities;
using Identity.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.LoginWithCode;

public record LoginByCodeDto(
	string? Mobile ,
	string? MobileActiveCode);

public record LoginWithCodeCommand(
	LoginByCodeDto data) :
	IRequest<Result<ApplicationUser>>;

public record LoginWithCodeCommandHandler(
	IUserService userService,
	UserManager<ApplicationUser> userManager,
	SignInManager<ApplicationUser> signInManager,
	IUserRepository userRepository ,
	IUnitOfWork unitOfWork) :
	IRequestHandler<LoginWithCodeCommand, Result<ApplicationUser>>
{
	public async Task<Result<ApplicationUser>> Handle(LoginWithCodeCommand request, CancellationToken cancellationToken)
	{
		#region Get User By Mobil

		var user = await userManager.FindByNameAsync(request.data.Mobile);
		if (user == null)
			return Result<ApplicationUser>.Failure("کاربری با اطلاعات وارد شده یافت نشد.");

		#endregion

		#region Validation Activation Code

		if (user.ActivationCode != request.data.MobileActiveCode)
			return Result<ApplicationUser>.Failure("کد وارد شده معتبر نمی باشد.");

		#endregion

		#region Update User 

		user.PhoneNumberConfirmed = true;
		user.ActivationCode = new Random().Next(10000, 999999).ToString();

		userRepository.Update(user);
		await unitOfWork.SaveChangesAsync(cancellationToken);

		#endregion

		await signInManager.SignInAsync(user , true);

		return Result<ApplicationUser>.Success(user);
	}
}
