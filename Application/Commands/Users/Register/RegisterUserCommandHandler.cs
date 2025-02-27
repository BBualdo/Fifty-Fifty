using Data;
using DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Commands.Users.Register;

public class RegisterUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher, IValidator<RegisterUserCommand> validator) : IRequestHandler<RegisterUserCommand, ValidationResult>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IValidator<RegisterUserCommand> _validator = validator;

    public async Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return new ValidationResult("Register failed", validationResult.Errors.Select(e => e.ErrorMessage));

        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            return new ValidationResult("Register failed", ["Email is already taken."]);

        if (await _context.Users.AnyAsync(u => u.Username.Trim().ToLower() == request.Username.Trim().ToLower(), cancellationToken))
            return new ValidationResult("Register failed", ["Username is already taken."]);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.Trim(),
            Username = request.Username.Trim(),
            PasswordHash = _passwordHasher.HashPassword(new User(), request.Password),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName?.Trim()
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ValidationResult();
    }
}
