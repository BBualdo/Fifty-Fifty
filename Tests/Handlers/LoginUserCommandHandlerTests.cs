using Application.Commands.Users.Login;
using Data;
using DTOs;
using FakeItEasy;
using FluentValidation;
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
    private readonly IValidator<LoginUserCommand> _validator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestDB").Options;
        _context = new AppDbContext(options);

        _passwordHasher = A.Fake<IPasswordHasher<User>>();
        A.CallTo(() => _passwordHasher.HashPassword(A<User>._, A<string>._))
            .Returns("hashedPassword");
        
        _validator = new LoginUserCommandValidator();

        _jwtService = A.Fake<IJwtService>();
        
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _handler = new LoginUserCommandHandler(_context, _passwordHasher, _validator, _jwtService);
    }

    [Theory]
    [InlineData("BBualdo", null)]
    [InlineData(null, "test@email.com")]
    public async Task Handle_ShouldLoginUser_WhenDataIsValid(string? username, string? email)
    {
        // Arrange
        var userBeforeLogin = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "BBualdo",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(userBeforeLogin);
        await _context.SaveChangesAsync();
        
        var command = new LoginUserCommand(username, email, "Test123!");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(userBeforeLogin, userBeforeLogin.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Success);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userBeforeLogin, userBeforeLogin.PasswordHash, command.Password);
        Assert.Equal(PasswordVerificationResult.Success, passwordVerificationResult);
        
        var userAfterLogin = await _context.Users.FindAsync(userBeforeLogin.Id);
        Assert.NotNull(userAfterLogin?.LastLoginAt);
    }

    [Fact]
    public async Task Handle_ShouldNotLoginUser_WhenUsernameAndEmailAreBothNull()
    {
        // Arrange
        var userBeforeLogin = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "BBualdo",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(userBeforeLogin);
        await _context.SaveChangesAsync();
        
        var command = new LoginUserCommand(null, null, "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Login attempt failed", result.Message);
        Assert.NotNull(result.ErrorList);
        Assert.Contains("Username or email is invalid.", result.ErrorList);
    }

    [Fact]
    public async Task Handle_ShouldNotLoginUser_WhenPasswordIsInvalid()
    {
        // Arrange
        var userBeforeLogin = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "BBualdo",
            PasswordHash = "hashedPassword"
        };
        await _context.Users.AddAsync(userBeforeLogin);
        await _context.SaveChangesAsync();
        
        var command = new LoginUserCommand(null, "test@email.com", "Test123");
        A.CallTo(() => _passwordHasher.VerifyHashedPassword(userBeforeLogin, userBeforeLogin.PasswordHash, command.Password))
            .Returns(PasswordVerificationResult.Failed);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Login attempt failed", result.Message);
        Assert.NotNull(result.ErrorList);
        Assert.Contains("Invalid password.", result.ErrorList);
        
        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userBeforeLogin, userBeforeLogin.PasswordHash, command.Password);
        Assert.Equal(PasswordVerificationResult.Failed, passwordVerificationResult);
    }

    [Fact]
    public async Task Handle_ShouldGenerateJwtToken_WhenLoginSuccessful()
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

        throw new NotImplementedException();
    }

    [Fact]
    public async Task Handle_ShouldGenerateAndStoreRefreshToken_WhenLoginSuccessful()
    {
        // Arrange
        
        
        // Act
        
        
        // Assert

        throw new NotImplementedException();
    }
}