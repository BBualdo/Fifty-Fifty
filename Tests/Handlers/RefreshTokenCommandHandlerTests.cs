using Application.Commands.Users.Refresh;
using Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers;

public class RefreshTokenCommandHandlerTests
{
    private readonly AppDbContext _context;

    private readonly User _dummyUser;
    private readonly RefreshTokenCommandHandler _handler;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestDB").Options;
        _context = new AppDbContext(options);
        _tokenService = A.Fake<ITokenService>();

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var userId = Guid.NewGuid();

        _dummyUser = new User
        {
            Id = userId,
            Email = "test@test.com",
            Username = "TestUser",
            PasswordHash = "hashedPassword",
            Role = UserRole.User,
            RefreshTokens = new List<RefreshToken>
            {
                new() { IsRevoked = false, IsUsed = false, Token = "validToken", UserId = userId },
                new() { IsRevoked = true, IsUsed = false, Token = "revokedToken", UserId = userId },
                new() { IsRevoked = false, IsUsed = true, Token = "usedToken", UserId = userId }
            }
        };

        _context.Users.Add(_dummyUser);
        _context.SaveChanges();

        _handler = new RefreshTokenCommandHandler(_context, _tokenService);
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
        var initialTokensNumber = _context.RefreshTokens.Count(rt => rt.UserId == _dummyUser.Id);
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

        var updatedTokensNumber = _context.RefreshTokens.Count(rt => rt.UserId == _dummyUser.Id);
        Assert.Equal(updatedTokensNumber, initialTokensNumber + 1);
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
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();

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
        return _dummyUser.RefreshTokens.FirstOrDefault(rt => rt.Token == tokenValue);
    }
}