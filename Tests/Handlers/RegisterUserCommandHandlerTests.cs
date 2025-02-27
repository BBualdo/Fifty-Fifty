using Application.Commands.Users.Register;
using Data;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Tests.Handlers;

public class RegisterUserCommandHandlerTests
{
    private readonly RegisterUserCommandHandler _handler;
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        _context = new AppDbContext(options);

        _passwordHasher = A.Fake<IPasswordHasher<User>>();
        A.CallTo(() => _passwordHasher.HashPassword(An<User>._, A<string>._))
                        .Returns("hashedPassword");

        _handler = new RegisterUserCommandHandler(_context, _passwordHasher);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange
        var command = new RegisterUserCommand("Sebastian", null, "BBualdo", "test@test.com", "Test123!");

        // Act
        await _handler.Handle(command, CancellationToken.None);
        var userCreatedAt = DateTime.UtcNow.Date;

        // Assert
        var addedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == command.Email);
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
}