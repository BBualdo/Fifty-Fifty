using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using Application.UseCases.Queries.Users;
using Domain.Entities;
using FakeItEasy;

namespace Tests.Handlers.Users;

public class CurrentLoggedInUserQueryHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserContext _userContext;
    private readonly CurrentLoggedInUserQueryHandler _handler;
    
    private readonly User _dummyUser;

    public CurrentLoggedInUserQueryHandlerTests()
    {
        _usersRepository = A.Fake<IUsersRepository>();
        _userContext = A.Fake<IUserContext>();

        _dummyUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            FirstName = "Test",
            LastName = "User",
            Username = "TestUser",
            Score = 20,
            Role = UserRole.User
        };

        A.CallTo(() => _usersRepository.GetByIdAsync(_dummyUser.Id, A<CancellationToken>._))
            .Returns(_dummyUser);
        A.CallTo(() => _userContext.UserId)
            .Returns(_dummyUser.Id);
        
        _handler = new CurrentLoggedInUserQueryHandler(_usersRepository, _userContext);
    }
}