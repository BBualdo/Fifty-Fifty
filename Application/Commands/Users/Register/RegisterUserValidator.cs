using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Commands.Users.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username can't be empty.")
            .MinimumLength(4)
            .WithMessage("Username must be at least 4 characters long.")
            .MaximumLength(32)
            .WithMessage("Username can't be longer than 32 characters.");

        RuleFor(x => x.Username)
            .Must(username => Regex.IsMatch(username.Trim(), "^[a-zA-Z0-9_]*$"))
            .WithMessage("Username can only contain letters, numbers and underscores.");

        RuleFor(x => x.Username)
            .Must(x => !x.Trim().Contains(" "))
            .WithMessage("Username can't contain spaces.");

        RuleFor(x => x.Username)
            .Must(x => !x.All(char.IsDigit))
            .WithMessage("Username can't contain only numbers.");
    }
}