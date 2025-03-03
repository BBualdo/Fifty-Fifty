using MediatR;
using Shared.DTO;

namespace Application.Commands.Users.Register;

public record RegisterUserCommand(string FirstName, string? LastName, string Username, string Email, string Password) : IRequest<Result<Guid>>;