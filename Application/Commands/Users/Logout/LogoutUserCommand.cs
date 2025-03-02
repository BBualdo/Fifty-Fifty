using DTOs;
using MediatR;

namespace Application.Commands.Users.Logout;

public record LogoutUserCommand(string RefreshToken, Guid UserId) : IRequest<Result<bool>>;