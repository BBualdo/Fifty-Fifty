using Domain.Entities;
using Shared.DTO;

namespace Application.Interfaces.Services.Auth;
   
public interface ITokenService
{
    JwtTokenDto GenerateJwtToken(User user);
    RefreshToken GenerateRefreshToken(User user);
}