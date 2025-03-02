using DTOs;
using Models;

namespace Services;
   
public interface ITokenService
{
    JwtTokenDto GenerateJwtToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}