using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using MediatR;
using Shared.DTO;
using Shared.Extensions;

namespace Application.UseCases.Queries.Users;

public class CurrentLoggedInUserQueryHandler(IUsersRepository usersRepository, IUserContext userContext) : IRequestHandler<CurrentLoggedInUserQuery, UserDto?>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IUserContext _userContext = userContext;
    
    public async Task<UserDto?> Handle(CurrentLoggedInUserQuery request, CancellationToken cancellationToken)
    {
        // Finds user based on ID obtained from sent JWT token if not null
        if (_userContext.UserId == null) return null;
        var user = await _usersRepository.GetByIdAsync(_userContext.UserId.Value, cancellationToken);
        
        // Returns required information if user exists
        return user?.ToUserDto();
    }
}