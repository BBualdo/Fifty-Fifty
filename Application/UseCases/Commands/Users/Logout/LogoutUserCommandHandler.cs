using Application.Interfaces.Repositories;
using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Logout;

public class LogoutUserCommandHandler(IRefreshTokensRepository refreshTokensRepository) : IRequestHandler<LogoutUserCommand, Result<bool>>
{
    private readonly IRefreshTokensRepository _refreshTokensRepository = refreshTokensRepository;
    
    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        // Checking if refresh token exists, is valid, not used or revoked and belongs to user
        var refreshToken = await _refreshTokensRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        
        if (refreshToken == null)
        {
            // TODO: Log that user used token that doesn't exist
        }
        else
        {
            if (refreshToken.UserId != request.UserId)
            {
                // TODO: Log that user used token that doesn't belong to him
            }
            
            if (refreshToken.IsRevoked)
            {
                // TODO: Log that user used revoked token
            }
            
            if (refreshToken.IsUsed)
            {
                // TODO: Log that user used token that was used before
            }

            if (refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                // TODO: Log that user used expired token
            }
            
            // Sets refresh token as revoked
            refreshToken.IsRevoked = true;
            await _refreshTokensRepository.SaveChangesAsync(cancellationToken);
        }
        
        // Returns success anyway to not inform user about any problems
        return Result<bool>.Success(true, "User logged out successfully.");
    }
}