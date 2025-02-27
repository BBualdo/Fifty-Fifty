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
        // Creating new user object
        // Hashing password
        // Saving user to database
        // Returning 201 Created status code
        throw new NotImplementedException();
    }
}