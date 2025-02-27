using Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Application.Commands.Users.Register;

public class RegisterUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher) : IRequestHandler<RegisterUserCommand>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    public async System.Threading.Tasks.Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
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
    }
}
