using Application.Commands.Users.Login;
using Data;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers;

public class LoginUserCommandHandlerTests
{
    private readonly AppDbContext _context;
    private readonly LoginUserCommandHandler _handler;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    private readonly User _dummyUser;

    public LoginUserCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("LoginUserCommandHandlerTests").Options;
        _context = new AppDbContext(options);

        _passwordHasher = A.Fake<IPasswordHasher<User>>();
        A.CallTo(() => _passwordHasher.HashPassword(A<User>._, A<string>._))
            .Returns("hashedPassword");

        _tokenService = A.Fake<ITokenService>();
        
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _handler = new LoginUserCommandHandler(_context, _passwordHasher, _tokenService);
        
        _dummyUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "BBualdo",
            PasswordHash = "hashedPassword"
        };
        _context.Users.Add(_dummyUser);
        _context.SaveChanges();
    }

    [Theory]
    [InlineData("BBualdo", null)]
    [InlineData(null, "test@email.com")]
    public async Task Handle_ShouldLoginUser_WhenDataIsValid(string? username, string? email)
    {
        // Arrange
        var command = new LoginUserCommand(username, email, "Test123!");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(_dummyUser, _dummyUser.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Success);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        var userAfterLogin = await _context.Users.FindAsync(_dummyUser.Id);
        Assert.NotNull(userAfterLogin?.LastLoginAt);
    }

    [Theory]
    [InlineData("Test", null)]
    [InlineData(null, "test@test.com")]
    public async Task Handle_ShouldNotRegisterUser_WhenDataIsNotValid(string? username, string? email)
    {
        // Arrange
        var command = new LoginUserCommand(username, email, "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Login attempt failed", result.Message);
        Assert.Contains("Username or email is invalid.", result.Errors!);
    }

    [Fact]
    public async Task Handle_ShouldNotLoginUser_WhenUsernameAndEmailAreBothNull()
    {
        // Arrange
        var command = new LoginUserCommand(null, null, "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Login attempt failed", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Username or email is invalid.", result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldNotLoginUser_WhenPasswordIsInvalid()
    {
        // Arrange
        var command = new LoginUserCommand(null, "test@email.com", "Test123");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(_dummyUser, _dummyUser.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Failed);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Login attempt failed", result.Message);
        Assert.NotNull(result.Errors);
        Assert.Contains("Invalid password.", result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldGenerateJwtToken_WhenLoginSuccessful()
    {
        // Arrange
        var command = new LoginUserCommand(null, "test@email.com", "Test123!");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(_dummyUser, _dummyUser.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Success);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        A.CallTo(() => _tokenService.GenerateJwtToken(_dummyUser))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_ShouldGenerateAndStoreRefreshToken_WhenLoginSuccessful()
    {
        // Arrange
        var command = new LoginUserCommand(null, "test@email.com", "Test123!");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(_dummyUser, _dummyUser.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Success);
        
        A.CallTo(() => _tokenService.GenerateRefreshToken(_dummyUser))
            .Returns(new RefreshToken
            {
                UserId = _dummyUser.Id,
            });
        var initialRefreshTokens = _context.RefreshTokens.Count(rt => rt.UserId == _dummyUser.Id);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        A.CallTo(() => _tokenService.GenerateRefreshToken((_dummyUser)))
            .MustHaveHappenedOnceExactly();
        
        var refreshTokens = _context.RefreshTokens.Count(rt => rt.UserId == _dummyUser.Id);
        Assert.Equal(initialRefreshTokens + 1, refreshTokens);
    }
}