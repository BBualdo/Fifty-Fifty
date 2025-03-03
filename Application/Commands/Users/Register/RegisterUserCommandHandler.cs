using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;
using Shared.Helpers;

namespace Application.Commands.Users.Register;

public class RegisterUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher, IValidator<RegisterUserCommand> validator) : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IValidator<RegisterUserCommand> _validator = validator;

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Checks if user information are formatted properly
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result<Guid>.Failure("Register failed", validationResult.Errors.Select(e => e.ErrorMessage));
        
        // Checks for username and email duplicates
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            return Result<Guid>.Failure("Register failed", ["Email is already taken."]);
        if (await _context.Users.AnyAsync(u => u.Username.Trim().ToLower() == request.Username.Trim().ToLower(), cancellationToken))
            return Result<Guid>.Failure("Register failed", ["Username is already taken."]);
        
        // Creates new user and saves it to database
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.Trim(),
            Username = request.Username.Trim(),
            PasswordHash = _passwordHasher.HashPassword(new User(), request.Password),
            FirstName = HelperFunctions.CapitalizeFirst(request.FirstName.Trim())!,
            LastName = HelperFunctions.CapitalizeFirst(request.LastName?.Trim())
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Returns created user ID
        return Result<Guid>.Success(user.Id, "User registered successfully");
    }
}
