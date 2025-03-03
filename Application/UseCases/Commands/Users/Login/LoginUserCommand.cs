using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Login;

public record LoginUserCommand(string? Username, string? Email, string Password) : IRequest<Result<TokenResponseDto>>;