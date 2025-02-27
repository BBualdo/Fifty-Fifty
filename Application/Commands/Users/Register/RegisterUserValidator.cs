using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Commands.Users.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        ConfigureUsernameRules();
        ConfigureEmailRules();
        ConfigureFirstNameRules();
        ConfigureLastNameRules();
    }

    private void ConfigureUsernameRules()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username can't be empty.")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters long.")
            .MaximumLength(32).WithMessage("Username can't be longer than 32 characters.")
            .Must(username => Regex.IsMatch(username.Trim(), "^[a-zA-Z0-9_]*$"))
            .WithMessage("Username can only contain letters, numbers and underscores.")
            .Must(username => !username.Trim().Contains(' '))
            .WithMessage("Username can't contain spaces.")
            .Must(username => !username.All(char.IsDigit))
            .WithMessage("Username can't contain only numbers.");
    }

    private void ConfigureEmailRules()
    {
        RuleFor(u => u.Email)
            .Matches(@"^(?!.*\.\.)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email address.");
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