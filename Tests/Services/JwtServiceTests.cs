using System.IdentityModel.Tokens.Jwt;
using DTOs;
using Models;
using Services;

namespace Tests.Services;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    
    public JwtServiceTests()
    {
        _jwtSettings = new JwtSettings
        {
            Secret = "superSecretKeyForTestingPurposesOnly123!",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationMinutes = 30,
        };

        _jwtService = new JwtService(_jwtSettings);
    }

    [Fact]
    public void GenerateToken_ShouldReturnValidJwtToken()
    {
        // Arrange
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "TestUser",
            Role = UserRole.User
        };
        
        // Act
        var token = _jwtService.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.False(string.IsNullOrEmpty(token));

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        
        Assert.Equal(jwtToken.Issuer, _jwtSettings.Issuer);
        Assert.Equal(jwtToken.Audiences.First(), _jwtSettings.Audience);
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "sub").Value, user.Id.ToString());
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "email").Value, user.Email);
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "role").Value, user.Role.ToString());
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "name").Value, user.Username);
    }
}