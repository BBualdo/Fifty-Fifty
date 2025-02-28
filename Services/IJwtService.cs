using Models;

namespace Services;
   
public interface IJwtService
{
    string GenerateToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}