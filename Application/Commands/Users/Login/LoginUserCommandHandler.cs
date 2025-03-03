using Application.Interfaces.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DTO;

namespace Application.Commands.Users.Login;

public class LoginUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher, ITokenService tokenService) : IRequestHandler<LoginUserCommand, Result<TokenResponseDto>>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly ITokenService _tokenService = tokenService;
    
    public async Task<Result<TokenResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Checks if user exists
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email, cancellationToken);
        if (user == null) 
            return Result<TokenResponseDto>.Failure("Login attempt failed", ["Username or email is invalid."]);
        
        // Matches password hashes
        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (passwordResult != PasswordVerificationResult.Success)
            return Result<TokenResponseDto>.Failure("Login attempt failed", ["Invalid password."]);
        
        // Updates last login date
        user.LastLoginAt = DateTimeOffset.UtcNow;
        
        // Generates JWT token
        var jwtToken = _tokenService.GenerateJwtToken(user);
        
        // Generates refresh token
        var refreshToken = _tokenService.GenerateRefreshToken(user);
        
        // Saves refresh token to database
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Returns JWT token and refresh token
        return Result<TokenResponseDto>.Success(new TokenResponseDto(jwtToken.Token, refreshToken.Token, jwtToken.ExpiresAt), "User logged in successfully");
    }
}