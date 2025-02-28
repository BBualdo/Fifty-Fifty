using DTOs;
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
}