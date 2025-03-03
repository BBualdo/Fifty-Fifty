using FluentValidation;
using Shared.Extensions;

namespace Application.UseCases.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .UsernameRules();
        RuleFor(u => u.Email)
            .EmailRules();
        ConfigureFirstNameRules();
        ConfigureLastNameRules();
        RuleFor(u => u.Password)
            .PasswordRules();
    }

    private void ConfigureFirstNameRules()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name can't be empty.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(32).WithMessage("First name can't be longer than 32 characters.")
            .Must(firstName => firstName.All(char.IsLetter)).WithMessage("First name can only contain letters.");
    }

    private void ConfigureLastNameRules()
    {
        RuleFor(u => u.LastName)
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(32).WithMessage("Last name can't be longer than 32 characters.")
            .Must(lastName => lastName.All(char.IsLetter)).WithMessage("Last name can only contain letters.")
            .When(u => !string.IsNullOrEmpty(u.LastName));
    }
}