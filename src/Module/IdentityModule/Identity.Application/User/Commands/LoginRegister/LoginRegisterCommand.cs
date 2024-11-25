using Framework.Application.Abstraction;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Commands.LoginRegister;

public record LoginRegisterDto(
	string Mobile ,
	string? ReturnUrl);

public record LoginRegisterCommand(
	LoginRegisterDto data) :
	IRequest<Result>;

public record LoginRegisterCommandHandler(
	IUserService userService,
	UserManager<ApplicationUser> userManager,
	IUnitOfWork unitOfWork) :
	IRequestHandler<LoginRegisterCommand, Result>
{
	private static readonly HttpClient client = new HttpClient();

	public async Task<Result> Handle(LoginRegisterCommand request, CancellationToken cancellationToken)
	{
		var user = new ApplicationUser();

		//Register User if user is not exist in data base 
		if (!await userService.IsExistUserByMobile(request.data.Mobile))
		{
			var mobile = request.data.Mobile.Trim().ToLower().SanitizeText();
			var password = PasswordHasher.EncodePasswordMd5(request.data.Mobile.SanitizeText());

			//Create User
			user = new User()
			{
				Password = password,
				Username = mobile,
				Mobile = mobile,
				EmailActivationCode = CodeGenerator.GenerateUniqCode(),
				MobileActivationCode = new Random().Next(10000, 999999).ToString(),
				ExpireMobileSMSDateTime = DateTime.Now
			};

			await UserRepository.AddUserWithoutSaveChange(user, cancellationToken);
			await unitOfWork.SaveChangesAsync(cancellationToken);
		}

		//Login User
		else
		{
			user = await userService.GetUserByMobile(request.data.Mobile);
			if (user == null || user.IsBan)
				return Result.Failure("شما دسترسی به سایت ندارید.");
		}

		#region Send Verification Code SMS

		var result = $"https://api.kavenegar.com/v1/564672526D58694D3477685571796F7372574F576C476B6366785462356D3164683370395A2B61356D6E383D/verify/lookup.json?receptor={user.Mobile}&token={user.MobileActivationCode}&template=Register";
		var results = client.GetStringAsync(result);

		#endregion

		return Result.Success;
	}
}
