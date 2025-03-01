using System.Text.RegularExpressions;
using FluentValidation;

namespace Utilities;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> UsernameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
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

    public static IRuleBuilderOptions<T, string> EmailRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^(?!.*\.\.)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .WithMessage("Invalid email address.");
    }

    public static IRuleBuilderOptions<T, string> PasswordRules<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Password can't be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(32).WithMessage("Password can't be longer than 32 characters.")
            .Must(password => password.Any(char.IsDigit))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")
            .Must(password => password.Any(char.IsLetter))
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")
            .Must(password => password.Any(char.IsUpper))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")
            .Must(password => password.Any(char.IsLower))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")
            .Must(password => !password.All(char.IsLetterOrDigit))
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
    }
}
