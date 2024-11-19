using FluentValidation;

namespace Identity.Application.User.Commands.Login;

public class UserLoginValidation : AbstractValidator<UserLoginCommand>
{
    public UserLoginValidation()
    {
        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty();
    }
}
