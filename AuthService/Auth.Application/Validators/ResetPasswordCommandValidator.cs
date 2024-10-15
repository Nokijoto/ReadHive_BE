using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public   ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");
        
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
        
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.");
    }
}