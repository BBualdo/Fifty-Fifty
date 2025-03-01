using DTOs;
using MediatR;

namespace Application.Commands.Users.Refresh;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResponseDto>>;