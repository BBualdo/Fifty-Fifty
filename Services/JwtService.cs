using DTOs;
using Models;

namespace Services;

public class JwtService(JwtSettings jwtSettings) : IJwtService
{
    private readonly JwtSettings _jwtSettings = jwtSettings;
    
    public string GenerateToken(User user)
    {
        throw new NotImplementedException();
    }

    public RefreshToken GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}