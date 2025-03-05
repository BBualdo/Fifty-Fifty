using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using Application.UseCases.Queries.Users;
using Domain.Entities;
using FakeItEasy;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers.Users;

public class CurrentLoggedInUserQueryHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserContext _userContext;
    private readonly CurrentLoggedInUserQueryHandler _handler;
    
    private readonly List<User> _usersStorage = [];
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
        _usersStorage.Add(_dummyUser);

        A.CallTo(() => _usersRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .ReturnsLazily((Guid id, CancellationToken _) => _usersStorage.FirstOrDefault(u => u.Id == id));
        
        _handler = new CurrentLoggedInUserQueryHandler(_usersRepository, _userContext);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserInfo_WhenUserExists()
    {
        // Arrange
        var query = new CurrentLoggedInUserQuery();
        A.CallTo(() => _userContext.UserId)
            .Returns(_dummyUser.Id);
        
        // Act
        var user = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.NotNull(user);
        Assert.Equal(_dummyUser.Id, user.Id);
        Assert.Equal(_dummyUser.Email, user.Email);
        Assert.Equal(_dummyUser.FirstName, user.FirstName);
        Assert.Equal(_dummyUser.LastName, user.LastName);
        Assert.Equal(_dummyUser.Username, user.Username);
        Assert.Equal(_dummyUser.Score, user.Score);
        Assert.Equal(_dummyUser.Role.ToString(), user.Role);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new CurrentLoggedInUserQuery();
        A.CallTo(() => _userContext.UserId)
            .Returns(Guid.NewGuid());
        
        // Act
        var user = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.Null(user);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnNull_WhenUserIsNotLoggedIn()
    {
        // Arrange
        var query = new CurrentLoggedInUserQuery();
        A.CallTo(() => _userContext.UserId)
            .Returns(null);
        
        // Act
        var user = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.Null(user);
        A.CallTo(() => _usersRepository.GetByIdAsync(_dummyUser.Id, A<CancellationToken>._))
            .MustNotHaveHappened();
    }
}