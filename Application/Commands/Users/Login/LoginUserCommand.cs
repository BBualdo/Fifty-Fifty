using MediatR;
using Shared.DTO;

namespace Application.Commands.Users.Login;

public record LoginUserCommand(string? Username, string? Email, string Password) : IRequest<Result<TokenResponseDto>>;