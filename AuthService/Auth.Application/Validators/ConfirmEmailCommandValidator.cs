using Application.Commands;
using FluentValidation;

namespace Application.Validators;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.userEmail)
            .NotEmpty().WithMessage("Email is required.");
        
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
    }   
}