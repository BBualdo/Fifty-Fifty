using MediatR;
using Shared.DTO;

namespace Application.UseCases.Queries.Users;

public record CurrentLoggedInUserQuery : IRequest<UserDto?>;