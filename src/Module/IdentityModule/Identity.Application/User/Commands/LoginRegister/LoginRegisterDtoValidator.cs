using FluentValidation;

namespace Identity.Application.User.Commands.LoginRegister;

public class LoginRegisterDtoValidator : AbstractValidator<LoginRegisterDto>
{
	public LoginRegisterDtoValidator()
	{
		RuleFor(dto => dto.Mobile)
			.NotEmpty()
			.WithMessage("این فیلد الزامی است .")
			.MaximumLength(20)
			.WithMessage("تعداد کاراکتر های {PropertyName} نمیتواند بیشتر از {MaxLength} باشد")
			.Matches(@"^([0-9]{11})$").WithMessage("موبایل وارد شده معتبر نمی باشد");
	}
}