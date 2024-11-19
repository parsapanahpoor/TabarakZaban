using FluentValidation;

namespace Identity.Application.User.Commands.ResetPassword;

public class ResetPasswordValidation : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);
    }
}

