using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Logout;

public record LogoutUserCommand(string RefreshToken) : IRequest<Result<bool>>;