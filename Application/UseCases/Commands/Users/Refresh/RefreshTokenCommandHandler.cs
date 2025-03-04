using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Refresh;

public class RefreshTokenCommandHandler(IRefreshTokensRepository refreshTokensRepository, ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly IRefreshTokensRepository _refreshTokensRepository = refreshTokensRepository;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Checks if refresh token exists, is valid, not used, revoked or expired and belongs to user
        var refreshToken = await _refreshTokensRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        
        if (refreshToken == null || refreshToken.IsUsed || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTimeOffset.UtcNow)
            return Result<TokenResponseDto>.Failure("Invalid token", ["Please try login again."]);
        
        // Sets refresh token as used
        refreshToken.IsUsed = true;
        
        // Generates new refresh token
        var freshRefreshToken = _tokenService.GenerateRefreshToken(refreshToken.User);
        
        // Saves it to database
        await _refreshTokensRepository.AddAsync(freshRefreshToken, cancellationToken);
        await _refreshTokensRepository.SaveChangesAsync(cancellationToken);
        
        // Generates new JWT token
        var freshJwtToken = _tokenService.GenerateJwtToken(refreshToken.User);
        
        // Returns new JWT token and refresh token
        return Result<TokenResponseDto>.Success(new TokenResponseDto(freshJwtToken.Token, refreshToken.Token, freshJwtToken.ExpiresAt), "Token successfully refreshed");
    }
}