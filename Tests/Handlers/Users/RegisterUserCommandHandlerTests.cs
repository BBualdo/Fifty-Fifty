﻿using Application.Interfaces.Repositories;
using Application.UseCases.Commands.Users.Register;
using Domain.Entities;
using FakeItEasy;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Shared.Helpers;
using Task = System.Threading.Tasks.Task;

namespace Tests.Handlers.Users;

public class RegisterUserCommandHandlerTests
{
    private readonly RegisterUserCommandHandler _handler;
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IValidator<RegisterUserCommand> _validator;
    
    private readonly List<User> _usersStorage = [];

    public RegisterUserCommandHandlerTests()
    {
        _passwordHasher = A.Fake<IPasswordHasher<User>>();
        _usersRepository = A.Fake<IUsersRepository>();
        
        A.CallTo(() => _passwordHasher.HashPassword(An<User>._, A<string>._))
                        .Returns("hashedPassword");
        A.CallTo(() => _usersRepository.AddAsync(A<User>._, A<CancellationToken>._))
            .Invokes((User user, CancellationToken _) => _usersStorage.Add(user));
        A.CallTo(() => _usersRepository.GetByEmailAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string email, CancellationToken _) => _usersStorage.FirstOrDefault(u => u.Email == email));
        A.CallTo(() => _usersRepository.GetByUsernameAsync(A<string>._, A<CancellationToken>._))
            .ReturnsLazily((string username, CancellationToken _) => _usersStorage.FirstOrDefault(u => u.Username == username));
        
        
        _validator = new RegisterUserCommandValidator();
        _handler = new RegisterUserCommandHandler(_usersRepository, _passwordHasher, _validator);
    }

    [Fact]
    public async Task Handle_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("Sebastian", null, "BBualdo", "test@test.com", "Test123!");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        var userCreatedAt = DateTime.UtcNow.Date;

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        var addedUser = await _usersRepository.GetByEmailAsync(command.Email, CancellationToken.None);
        Assert.NotNull(addedUser);

        Assert.Equal(command.FirstName, addedUser.FirstName);
        Assert.Equal(command.LastName, addedUser.LastName);
        Assert.Equal(command.Username, addedUser.Username);
        Assert.Equal(command.Email, addedUser.Email);
        Assert.Equal("hashedPassword", addedUser.PasswordHash);
        Assert.Equal(0, addedUser.Score);
        Assert.Equal(userCreatedAt, addedUser.CreatedAt.Date);
        Assert.Null(addedUser.LastLoginAt);
        Assert.Equal(UserRole.User, addedUser.Role);

        Assert.Empty(addedUser.Households);
        Assert.Empty(addedUser.ReceivedInvitations);
        Assert.Empty(addedUser.SentInvitations);
        Assert.Empty(addedUser.RefreshTokens);
    }

    [Fact]
    public async Task Handle_ShouldCapitalizeFirstNameAndLastName()
    {
        // Arrange
        var command = new RegisterUserCommand("sebastian", "testing", "BBualdo", "test@test.com", "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        
        var addedUser = await _usersRepository.GetByEmailAsync(command.Email, CancellationToken.None);
        Assert.NotNull(addedUser);
        Assert.Equal(HelperFunctions.CapitalizeFirst(command.FirstName), addedUser.FirstName); 
        Assert.Equal(HelperFunctions.CapitalizeFirst(command.LastName), addedUser.LastName);
    }

    [Fact]
    public async Task Handle_ShouldNotRegisterUser_WhenEmailIsAlreadyTaken()
    {
        // Arrange
        var user1 = new User() { 
            Id = Guid.NewGuid(),
            Email = "user1@test.com",
            Username = "user1",
            PasswordHash = "hashedPassword",
            FirstName = "User",
        };

        _usersStorage.Add(user1);

        var command = new RegisterUserCommand("User", null, "user2", "user1@test.com", "Test123!");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Equal("Email is already taken.", result.Errors!.First());

        var usersWithSameEmail = _usersStorage.Count(u => u.Email == command.Email);
        Assert.Equal(1, usersWithSameEmail);
    }

    [Theory]
    [InlineData("user1", "user1")]
    [InlineData("user1", "UsER1")]
    [InlineData("user1", "  UsER1")]
    public async Task Handle_ShouldNotRegisterUser_WhenUsernameIsAlreadyTaken(string username1, string username2)
    {
        // Arrange
        var user1 = new User()
        {
            Id = Guid.NewGuid(),
            Email = "user1@test.com",
            Username = username1,
            PasswordHash = "hashedPassword",
            FirstName = "User",
        };

        _usersStorage.Add(user1);

        var command = new RegisterUserCommand("User", null, username2, "user2@test.com", "Test123!");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Equal("Username is already taken.", result.Errors!.First());

        var usersWithSameUsername = _usersStorage.Count(u => u.Username == username1);
        Assert.Equal(1, usersWithSameUsername);
    }

    [Theory]
    [InlineData("user~%2", "Username can only contain letters, numbers and underscores.")]
    [InlineData("user1@", "Username can only contain letters, numbers and underscores.")]
    [InlineData("user'\\", "Username can only contain letters, numbers and underscores.")]
    [InlineData("USER+=", "Username can only contain letters, numbers and underscores.")]    
    [InlineData("user 1", "Username can't contain spaces.")]
    [InlineData("", "Username can't be empty.")]
    [InlineData("   ", "Username can't be empty.")]
    [InlineData("12345678", "Username can't contain only numbers.")]
    [InlineData("u", "Username must be at least 4 characters long.")]
    [InlineData("super_duper_long_username1234567890", "Username can't be longer than 32 characters.")]

    public async Task Handle_ShouldNotRegisterUser_WhenInvalidUsername(string username, string errorMessage)
    {
        // Arrange
        var command = new RegisterUserCommand("User", null, username, "test@test.com", "Test123!");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Contains(errorMessage, result.Errors!);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@com")]
    [InlineData("test.com")]
    [InlineData("test@.com")]
    [InlineData("test@com.")]
    [InlineData("test\\s@test.com")]
    [InlineData("test@te st.com")]
    public async Task Handle_ShouldNotRegisterUser_WhenInvalidEmail(string email)
    {
        // Arrange
        var command = new RegisterUserCommand("User", null, "user123", email, "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Contains("Invalid email address.", result.Errors!.First());
    }

    [Theory]
    [InlineData("", "First name can't be empty.")]
    [InlineData("   ", "First name can't be empty.")]
    [InlineData("s", "First name must be at least 2 characters long.")]
    [InlineData("thelongestnameintheworldcoveredbytestsanyway", "First name can't be longer than 32 characters.")]
    [InlineData("12315151", "First name can only contain letters.")]
    [InlineData("test name", "First name can only contain letters.")]
    [InlineData("test@name", "First name can only contain letters.")]
    [InlineData("test_name", "First name can only contain letters.")]
    public async Task Handle_ShouldNotRegisterUser_WhenInvalidFirstName(string firstName, string errorMessage)
    {
        // Arrange
        var command = new RegisterUserCommand(firstName, null, "TestUser", "test@test.com", "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Contains(errorMessage, result.Errors!);
    }

    [Theory]
    [InlineData("s", "Last name must be at least 2 characters long.")]
    [InlineData("thelongestnameintheworldcoveredbytestsanyway", "Last name can't be longer than 32 characters.")]
    [InlineData("12315151", "Last name can only contain letters.")]
    [InlineData("test name", "Last name can only contain letters.")]
    [InlineData("test@name", "Last name can only contain letters.")]
    [InlineData("test_name", "Last name can only contain letters.")]
    public async Task Handle_ShouldNotRegisterUser_WhenInvalidLastName(string lastName, string errorMessage)
    {
        // Arrange
        var command = new RegisterUserCommand("Test", lastName, "TestUser", "test@test.com", "Test123!");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Contains(errorMessage, result.Errors!);
    }

    [Theory]
    [InlineData("", "Password can't be empty.")]
    [InlineData("   ", "Password can't be empty.")]
    [InlineData("s", "Password must be at least 6 characters long.")]
    [InlineData("SuperLongPasswordIsImportantButThereAreLimits", "Password can't be longer than 32 characters.")]
    [InlineData("weakpassword",
        "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    [InlineData("WeakPassword",
        "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    [InlineData("WeakPassword1",
        "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public async Task Handle_ShouldNotRegisterUser_WhenInvalidPassword(string password, string errorMessage)
    {
        // Arrange
        var command = new RegisterUserCommand("Test", null, "TestUser", "test@test.com", password);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Equal("Register failed", result.Message);
        Assert.Contains(errorMessage, result.Errors!);
    }
}