using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Logout;

public record LogoutUserCommand(string RefreshToken, Guid UserId) : IRequest<Result<bool>>;