namespace DTOs;

public class TokenResponseDto(string jwtToken, string refreshToken)
{
    public string JwtToken { get; private set; } = jwtToken;
    public string RefreshToken { get; private set; } = refreshToken;
}