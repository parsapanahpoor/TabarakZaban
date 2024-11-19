using FluentValidation;

namespace Identity.Application.User.Commands.RefreshToken;

public class RefreshTokenValidation : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidation()
    {
        RuleFor(x => x.RefreshToken)
        .NotEmpty();
    }
}
