using Framework.Application.Abstraction;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace Identity.Application.User.Queries.LoginWithCode;

public record LoginWithCodeQuery(
	string Mobile,
	bool Resend) :
	IRequest<Result<ApplicationUser>>;

public record LoginWithCodeQueryHandler(
	IUserService userService , 
	UserManager<ApplicationUser> userManager , 
	IUnitOfWork unitOfWork) : 
	IRequestHandler<LoginWithCodeQuery , Result<ApplicationUser>>
{
	private static readonly HttpClient client = new HttpClient();

	public async Task<Result<ApplicationUser>> Handle(LoginWithCodeQuery request, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByNameAsync(request.Mobile);
		if (user == null)
			return Result<ApplicationUser>.Failure("موبایل وارد شده معتبر نمی باشد.");

		if (request.Resend)
		{
			user.ActivationCode = new Random().Next(10000, 999999).ToString();
			user.ExpireMobileSMSDateTime = DateTime.Now;

			await unitOfWork.SaveChangesAsync();

			#region Send Verification Code SMS

			var result = $"https://api.kavenegar.com/v1/564672526D58694D3477685571796F7372574F576C476B6366785462356D3164683370395A2B61356D6E383D/verify/lookup.json?receptor={user.PhoneNumber}&token={user.ActivationCode}&template=Register";
			var results = client.GetStringAsync(result);

			#endregion
		}

		return Result<ApplicationUser>.Success(user);
	}
}
