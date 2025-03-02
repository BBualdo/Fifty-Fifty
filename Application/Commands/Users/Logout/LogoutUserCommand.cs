using DTOs;
using MediatR;

namespace Application.Commands.Users.Logout;

public record LogoutUserCommand(string RefreshToken) : IRequest<Result<bool>>;