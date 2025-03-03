namespace Shared.DTO;

public class TokenResponseDto(string jwtToken, string refreshToken, long expiresAt)
{
    public string JwtToken { get; private set; } = jwtToken;
    public string RefreshToken { get; private set; } = refreshToken;
    public long ExpiresAt { get; private set; } = expiresAt;
}