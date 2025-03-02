using Data;
using DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.Logout;

public class LogoutUserCommandHandler(AppDbContext context) : IRequestHandler<LogoutUserCommand, Result<bool>>
{
    private readonly AppDbContext _context = context;
    
    public async Task<Result<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        // Checking if refresh token exists, is valid, not used or revoked and belongs to user
        var refreshToken = _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefault(rt => rt.Token == request.RefreshToken);
        
        if (refreshToken == null)
        {
            // TODO: Log that user used token that doesn't exist
        }
        else
        {
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
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        // Returns success anyway to not inform user about any problems
        return Result<bool>.Success(true, "User logged out successfully.");
    }
}