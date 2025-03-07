using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Auth;
using MediatR;
using Shared.DTO;

namespace Application.UseCases.Commands.Users.Logout;

public class LogoutUserCommandHandler(IRefreshTokensRepository refreshTokensRepository, IUserContext userContext) : IRequestHandler<LogoutUserCommand, Result<bool>>
{
    private readonly IRefreshTokensRepository _refreshTokensRepository = refreshTokensRepository;
    private readonly IUserContext _userContext = userContext;
    
    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokensRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        if (refreshToken == null)
        {
            // TODO: Log that user used token that doesn't exist
            return Result<bool>.Success(true, "User logged out successfully.");
        }
        
        // Checking if refresh token exists, is valid, not used or revoked and belongs to user
        var userId = _userContext.UserId;
        if (userId == null)
        {
            // TODO: Log that logout has been invoked without JWT or with expired one
        }
        
        if (refreshToken.UserId != userId)
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
        
        // Returns success anyway to not inform user about any problems
        return Result<bool>.Success(true, "User logged out successfully.");
    }
}