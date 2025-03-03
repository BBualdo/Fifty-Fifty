using Application.Interfaces.Services;
using MediatR;
using Shared.DTO;

namespace Application.Commands.Users.Refresh;

public class RefreshTokenCommandHandler(AppDbContext context, ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly AppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Checks if refresh token exists, is valid, not used, revoked or expired and belongs to user
        var refreshToken = _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefault(rt => rt.Token == request.RefreshToken);
        
        if (refreshToken == null || refreshToken.IsUsed || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTimeOffset.UtcNow)
            return Result<TokenResponseDto>.Failure("Invalid token", ["Please try login again."]);
        
        // Sets refresh token as used
        refreshToken.IsUsed = true;
        
        // Generates new refresh token
        var freshRefreshToken = _tokenService.GenerateRefreshToken(refreshToken.User);
        
        // Saves it to database
        await _context.RefreshTokens.AddAsync(freshRefreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Generates new JWT token
        var freshJwtToken = _tokenService.GenerateJwtToken(refreshToken.User);
        
        // Returns new JWT token and refresh token
        return Result<TokenResponseDto>.Success(new TokenResponseDto(freshJwtToken.Token, refreshToken.Token, freshJwtToken.ExpiresAt), "Token successfully refreshed");
    }
}