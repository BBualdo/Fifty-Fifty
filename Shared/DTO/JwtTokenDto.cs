namespace Shared.DTO;

public class JwtTokenDto(string token, long expiresAt)
{
    public string Token { get; set; } = token;
    public long ExpiresAt { get; set; } = expiresAt;
}