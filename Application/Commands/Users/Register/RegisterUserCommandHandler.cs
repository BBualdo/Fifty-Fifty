using Data;
using DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Application.Commands.Users.Register;

public class RegisterUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher) : IRequestHandler<RegisterUserCommand, ValidationResult>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    public async Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            return new ValidationResult("Register failed", ["Email is already taken."]);

        if (await _context.Users.AnyAsync(u => u.Username.Trim().ToLower() == request.Username.Trim().ToLower(), cancellationToken))
            return new ValidationResult("Register failed", ["Username is already taken."]);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(new User(), request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ValidationResult();
    }
}
