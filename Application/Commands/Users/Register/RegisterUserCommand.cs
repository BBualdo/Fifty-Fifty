using MediatR;
using DTOs;

namespace Application.Commands.Users.Register;

public record RegisterUserCommand(string FirstName, string? LastName, string Username, string Email, string Password) : IRequest<ValidationResult>;