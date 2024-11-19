using FluentValidation;
using Identity.Application.User.Commands.Login;

namespace Identity.Application.User.Commands.ConfirmEmail;

public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidation()
    {
        RuleFor(x => x.UserId)
        .NotEmpty();

        RuleFor(x => x.Tokken)
        .NotEmpty();
    }
}
