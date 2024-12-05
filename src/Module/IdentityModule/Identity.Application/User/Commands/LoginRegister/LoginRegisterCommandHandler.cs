using Framework.Application.Abstraction;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;
using SharedProject.Security;

namespace Identity.Application.User.Commands.LoginRegister;

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
		if (await userManager.FindByNameAsync(request.data.Mobile) is null)
		{
			var applicationUser = new ApplicationUser()
			{
				UserName = request.data.Mobile.Trim().ToLower(),
				PhoneNumber = request.data.Mobile.Trim().ToLower(),
				ActivationCode = new Random().Next(10000, 999999).ToString(),
			};

			var res = await userManager.CreateAsync(applicationUser, PasswordHasher.EncodePasswordMd5(request.data.Mobile));
			if (!res.Succeeded)
				Result.Failure(
					new Error(
						code: string.Join(",", res.Errors.Select(p => p.Code)),
						message: string.Join(",", res.Errors.Select(p => p.Description))));
		}

		//Login User
		else
		{
			user = await userManager.FindByNameAsync(request.data.Mobile);
			if (user == null || user.IsBan)
				return Result.Failure("شما دسترسی به سایت ندارید.");
		}

		#region Send Verification Code SMS

		var result = $"https://api.kavenegar.com/v1/564672526D58694D3477685571796F7372574F576C476B6366785462356D3164683370395A2B61356D6E383D/verify/lookup.json?receptor={user.PhoneNumber}&token={user.ActivationCode}&template=Register";
		var results = client.GetStringAsync(result);

		#endregion

		return Result.Success;
	}
}
