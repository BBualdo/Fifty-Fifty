using System.IdentityModel.Tokens.Jwt;
using Domain.Entities;
using Infrastructure.Services;
using Shared;

namespace Tests.Services;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;
    private readonly JwtSettings _jwtSettings;
    
    public TokenServiceTests()
    {
        _jwtSettings = new JwtSettings
        {
            Secret = "superSecretKeyForTestingPurposesOnly123!",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationMinutes = 30,
        };

        _tokenService = new TokenService(_jwtSettings);
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
        var jwtTokenDto = _tokenService.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(jwtTokenDto.Token);
        Assert.False(string.IsNullOrEmpty(jwtTokenDto.Token));

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(jwtTokenDto.Token);
        
        Assert.Equal(jwtToken.Issuer, _jwtSettings.Issuer);
        Assert.Equal(jwtToken.Audiences.First(), _jwtSettings.Audience);
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "sub").Value, user.Id.ToString());
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "email").Value, user.Email);
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "role").Value, user.Role.ToString());
        Assert.Equal(jwtToken.Claims.First(c => c.Type == "name").Value, user.Username);
        Assert.NotNull(jwtToken.Claims.First(c => c.Type == "iat").Value);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void GenerateRefreshToken_ShouldReturnUniqueRefreshTokenEveryTime(int iterationCount)
    {
        // Arrange
        var tokens = new List<string>();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            Username = "TestUser",
        };
        
        // Act
        for (var i = 0; i < iterationCount; i++)
            tokens.Add(_tokenService.GenerateRefreshToken(user).Token);  
        
        // Assert
        Assert.NotEmpty(tokens);
        Assert.Equal(tokens.Count, new HashSet<string>(tokens).Count);
    }
}