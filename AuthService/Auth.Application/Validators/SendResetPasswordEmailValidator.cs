using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class SendResetPasswordEmailValidator : AbstractValidator<SendResetPasswordEmailCommand>
{
    public SendResetPasswordEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required. YOKLOOOOO");
    }
}