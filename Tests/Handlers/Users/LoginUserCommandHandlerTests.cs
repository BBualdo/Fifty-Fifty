using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using Application.UseCases.Commands.Users.Login;
using Domain.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers.Users;

public class LoginUserCommandHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRefreshTokensRepository _refreshTokensRepository;
    private readonly LoginUserCommandHandler _handler;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;
    
    private readonly User _dummyUser;
    private readonly List<RefreshToken> _refreshTokensStorage;
    private readonly List<User> _usersStorage;

    public LoginUserCommandHandlerTests()
    {
        _passwordHasher = A.Fake<IPasswordHasher<User>>();
        _tokenService = A.Fake<ITokenService>();
        _usersRepository = A.Fake<IUsersRepository>();
        _refreshTokensRepository = A.Fake<IRefreshTokensRepository>();
        
        _dummyUser = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "BBualdo",
            PasswordHash = "hashedPassword"
        };

        _refreshTokensStorage = [];
        _usersStorage = [ _dummyUser ];

        A.CallTo(() => _usersRepository.GetByEmailAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string email, CancellationToken _) => _usersStorage.FirstOrDefault(u => u.Email == email));
        A.CallTo(() => _usersRepository.GetByUsernameAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string username, CancellationToken _) => _usersStorage.FirstOrDefault(u => u.Username == username));
        A.CallTo(() => _refreshTokensRepository.GetAllByUserIdAsync(_dummyUser.Id, A<CancellationToken>._))
            .Returns(_refreshTokensStorage);
        A.CallTo(() => _refreshTokensRepository.AddAsync(A<RefreshToken>._, A<CancellationToken>._))
            .Invokes((RefreshToken token, CancellationToken _) => _refreshTokensStorage.Add(token));
        
        
        _handler = new LoginUserCommandHandler(_usersRepository, _refreshTokensRepository, _passwordHasher, _tokenService);
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
        var userAfterLogin = email != null 
            ? await _usersRepository.GetByEmailAsync(email, CancellationToken.None) : username != null
            ? await _usersRepository.GetByUsernameAsync(username, CancellationToken.None) : null;
        
        Assert.NotNull(userAfterLogin);
        Assert.NotNull(userAfterLogin.LastLoginAt);
    }

    [Theory]
    [InlineData("Test", null)]
    [InlineData(null, "test@test.com")]
    public async Task Handle_ShouldNotLoginUser_WhenDataIsNotValid(string? username, string? email)
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
            .Returns(new RefreshToken { UserId = _dummyUser.Id });
        
        A.CallTo(() => _refreshTokensRepository.GetAllByUserIdAsync(_dummyUser.Id, A<CancellationToken>._))
            .Returns(_dummyUser.RefreshTokens);
        
        var initialRefreshTokens = _refreshTokensStorage.Count;
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        A.CallTo(() => _tokenService.GenerateRefreshToken((_dummyUser)))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _refreshTokensRepository.AddAsync(A<RefreshToken>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        
        Assert.Equal(initialRefreshTokens + 1, _refreshTokensStorage.Count);
    }
}