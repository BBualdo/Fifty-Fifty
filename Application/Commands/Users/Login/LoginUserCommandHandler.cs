using Data;
using DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

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
        user.LastLoginAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Generates JWT token
        var jwtToken = _tokenService.GenerateJwtToken(user);
        // Generates refresh token
        var refreshToken = _tokenService.GenerateRefreshToken(user);
        
        // Saves refresh token to database
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Returns JWT token and refresh token
        return Result<TokenResponseDto>.Success(new TokenResponseDto(jwtToken, refreshToken.Token), "User logged in successfully");
    }
}