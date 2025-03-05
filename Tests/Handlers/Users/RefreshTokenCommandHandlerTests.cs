using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using Application.UseCases.Commands.Users.Refresh;
using Domain.Entities;
using FakeItEasy;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers.Users;

public class RefreshTokenCommandHandlerTests
{
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly RefreshTokenCommandHandler _handler;
    private readonly ITokenService _tokenService;
    
    private readonly User _dummyUser;
    private readonly List<RefreshToken> _refreshTokensStorage = [];
    
    public RefreshTokenCommandHandlerTests()
    {
        _refreshTokensRepository = A.Fake<IRefreshTokensRepository>();
        _tokenService = A.Fake<ITokenService>();

        var userId = Guid.NewGuid();

        _dummyUser = new User
        {
            Id = userId,
            Email = "test@test.com",
            Username = "TestUser",
            PasswordHash = "hashedPassword",
            Role = UserRole.User,
            RefreshTokens = _refreshTokensStorage
        };

        _refreshTokensStorage.AddRange([
            new RefreshToken
            {
                IsRevoked = false, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "validToken",
                UserId = userId, User = _dummyUser
            },
            new RefreshToken
            {
                IsRevoked = true, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "revokedToken",
                UserId = userId, User = _dummyUser
            },
            new RefreshToken
            {
                IsRevoked = false, IsUsed = true, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "usedToken",
                UserId = userId, User = _dummyUser
            },
            new RefreshToken
            {
                IsRevoked = false, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(-1),
                Token = "expiredToken", UserId = userId, User = _dummyUser
            }
        ]);

        A.CallTo(() => _refreshTokensRepository.GetByTokenAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string token, CancellationToken _) =>
                _refreshTokensStorage.FirstOrDefault(t => t.Token == token));
        A.CallTo(() => _refreshTokensRepository.AddAsync(A<RefreshToken>._, A<CancellationToken>._))
            .Invokes((RefreshToken token, CancellationToken _) => _refreshTokensStorage.Add(token));

        _handler = new RefreshTokenCommandHandler(_refreshTokensRepository, _tokenService);
    }

    [Fact]
    public async Task Handle_ShouldMarkTokenAsUsed_WhenValidToken()
    {
        // Arrange
        var token = GetToken("validToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        var updatedToken = GetToken("validToken");
        Assert.NotNull(updatedToken);
        Assert.True(updatedToken.IsUsed);
        Assert.False(updatedToken.IsRevoked);
    }

    [Fact]
    public async Task Handle_ShouldGenerateNewRefreshToken_WhenValidToken()
    {
        // Arrange
        var token = GetToken("validToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ShouldGenerateNewJwtToken_WhenValidToken()
    {
        // Arrange
        var token = GetToken("validToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ShouldStoreNewRefreshToken_WhenValidToken()
    {
        // Arrange
        var token = GetToken("validToken");
        var command = new RefreshTokenCommand(token!.Token);
        var initialTokensNumber = _refreshTokensStorage.Count;
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser))
            .Returns(new RefreshToken
            {
                Token = "newToken",
                UserId = _dummyUser.Id
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(initialTokensNumber + 1, _refreshTokensStorage.Count);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenIsRevoked()
    {
        // Arrange
        var token = GetToken("revokedToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Please try login again.", result.Errors);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustNotHaveHappened();
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenIsUsed()
    {
        // Arrange
        var token = GetToken("usedToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Please try login again.", result.Errors);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustNotHaveHappened();
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenIsExpired()
    {
        // Arrange
        var token = GetToken("expiredToken");
        var command = new RefreshTokenCommand(token!.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Please try login again.", result.Errors);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustNotHaveHappened();
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenDoesntExist()
    {
        // Arrange
        var token = "notExistingToken";
        var command = new RefreshTokenCommand(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Please try login again.", result.Errors);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustNotHaveHappened();
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenDoesntBelongToUser()
    {
        // Arrange
        var token = new RefreshToken
        {
            UserId = Guid.NewGuid()
        };
        _refreshTokensStorage.Add(token);

        var command = new RefreshTokenCommand(token.Token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Please try login again.", result.Errors);
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser)).MustNotHaveHappened();
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser)).MustNotHaveHappened();
    }

    private RefreshToken? GetToken(string tokenValue)
    {
        return _refreshTokensStorage.FirstOrDefault(rt => rt.Token == tokenValue);
    }
}