using Application.Commands.Users.Logout;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers;

public class LogoutUserCommandHandlerTests
{
    private readonly AppDbContext _context;
    private readonly LogoutUserCommandHandler _handler;
    
    private readonly User _dummyUser;
    
    public LogoutUserCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("LogoutUserCommandHandlerTests").Options;
        _context = new AppDbContext(options);
        
        var userId = Guid.NewGuid();
        _dummyUser = new User
        {
            Id = userId,
            Email = "test@email.com",
            Username = "TestUser",
            PasswordHash = "hashedPassword",
            Role = UserRole.User,
            RefreshTokens = new List<RefreshToken>
            {
                new() { IsRevoked = false, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "validToken", UserId = userId },
                new() { IsRevoked = true, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "revokedToken", UserId = userId },
                new() { IsRevoked = false, IsUsed = true, ExpiresAt = DateTimeOffset.UtcNow.AddDays(7), Token = "usedToken", UserId = userId },
                new() { IsRevoked = false, IsUsed = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(-1), Token = "expiredToken", UserId = userId },
            }
        };
        
        _context.Users.Add(_dummyUser);
        _context.SaveChanges();
        
        _handler = new LogoutUserCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_ShouldRevokeRefreshToken_WhenTokenIsValid()
    {
        // Arrange
        var token = GetToken("validToken");
        var command = new LogoutUserCommand(token!.Token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        
        var updatedToken = GetToken("validToken");
        Assert.NotNull(updatedToken);
        Assert.True(updatedToken.IsRevoked);
    }
    
    [Fact]
    public async Task Handle_ShouldLogoutUser_WhenTokenIsRevoked()
    {
        // Arrange
        var token = GetToken("revokedToken");
        var command = new LogoutUserCommand(token!.Token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        // TODO: Check for logging that user used revoked token
    }
    
    [Fact]
    public async Task Handle_ShouldLogoutUser_WhenTokenIsUsed()
    {
        // Arrange
        var token = GetToken("usedToken");
        var command = new LogoutUserCommand(token!.Token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        // TODO: Check for logging that user used used token
    }
    
    [Fact]
    public async Task Handle_ShouldLogoutUser_WhenTokenIsExpired()
    {
        // Arrange
        var token = GetToken("expiredToken");
        var command = new LogoutUserCommand(token!.Token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        // TODO: Check for logging that user used expired token
    }
    
    [Fact]
    public async Task Handle_ShouldLogoutUser_WhenTokenDoesntExist()
    {
        // Arrange
        var token = "notExistingToken";
        var command = new LogoutUserCommand(token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        // TODO: Check for logging that user used not existing token
    }
    
    [Fact]
    public async Task Handle_ShouldLogoutUser_WhenTokenDoesntBelongToUser()
    {
        // Arrange
        var token = new RefreshToken
        {
            UserId = Guid.NewGuid()
        };
        await _context.RefreshTokens.AddAsync(token);
        await _context.SaveChangesAsync();
        
        var command = new LogoutUserCommand(token.Token, _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        // TODO: Check for logging that user used token that doesn't belong to him
    }
    
    private RefreshToken? GetToken(string tokenValue)
    {
        return _context.RefreshTokens.FirstOrDefault(rt => rt.Token == tokenValue);
    }
}