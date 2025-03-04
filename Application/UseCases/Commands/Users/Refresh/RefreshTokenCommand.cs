using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Refresh;

public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResponseDto>>;