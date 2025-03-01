using Data;
using DTOs;
using MediatR;
using Services;

namespace Application.Commands.Users.Refresh;

public class RefreshTokenCommandHandler(AppDbContext context, ITokenService tokenService)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    private readonly AppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // Checking if refresh token exists, is valid, not used or revoked and belongs to user
        var refreshToken = _context.RefreshTokens.SingleOrDefault(t => t.Token == request.RefreshToken);
        if (refreshToken == null || refreshToken.IsUsed || refreshToken.IsRevoked)
            return Result<TokenResponseDto>.Failure("Invalid token", ["Please try login again."]);
        var user = _context.Users.SingleOrDefault(u => u.Id == refreshToken.UserId);
        if (user == null || refreshToken.UserId != user.Id)
            return Result<TokenResponseDto>.Failure("Invalid token", ["Please try login again."]);
        // Setting refresh token as used
        refreshToken.IsUsed = true;
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Generating new refresh token
        var freshRefreshToken = _tokenService.GenerateRefreshToken(user);
        
        // Saving it to database
        await _context.RefreshTokens.AddAsync(freshRefreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Generating new JWT token
        var freshJwtToken = _tokenService.GenerateJwtToken(user);
        
        // Returning new JWT token and refresh token (200)
        return Result<TokenResponseDto>.Success(new TokenResponseDto(freshJwtToken, freshRefreshToken.Token), "Token successfully refreshed");
    }
}