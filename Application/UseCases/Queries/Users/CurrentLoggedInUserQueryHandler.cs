using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using MediatR;
using Shared.DTO;

namespace Application.UseCases.Queries.Users;

public class CurrentLoggedInUserQueryHandler(IUsersRepository usersRepository, IUserContext userContext) : IRequestHandler<CurrentLoggedInUserQuery, UserDto>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IUserContext _userContext = userContext;
    
    public Task<UserDto> Handle(CurrentLoggedInUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}