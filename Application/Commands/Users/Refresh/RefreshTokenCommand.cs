using MediatR;
using Shared.DTO;

namespace Application.Commands.Users.Refresh;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResponseDto>>;