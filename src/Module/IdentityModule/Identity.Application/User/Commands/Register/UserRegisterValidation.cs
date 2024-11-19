using FluentValidation;

namespace Identity.Application.User.Commands.Register;

public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(4);

        RuleFor(x => x.ConfirmPassword)
        .Equal(x => x.Password);
    }
}

