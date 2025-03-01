using Models;

namespace Services;
   
public interface ITokenService
{
    string GenerateJwtToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}