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
        // Checking if refresh token exists, is valid, not user or revoked and belongs to user
        // Setting refresh token as used
        // Generating new refresh token
        // Saving it to database
        // Generating new JWT token
        // Returning new JWT token and refresh token (200)
        throw new NotImplementedException();
    }
}