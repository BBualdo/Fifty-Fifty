using Data;
using DTOs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Application.Commands.Users.Login;

public class LoginUserCommandHandler(AppDbContext context, IPasswordHasher<User> passwordHasher, IValidator<LoginUserCommand> validator, IJwtService jwtService) : IRequestHandler<LoginUserCommand, Result<TokenResponseDto>>
{
    private readonly AppDbContext _context = context;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IValidator<LoginUserCommand> _validator = validator;
    private readonly IJwtService _jwtService = jwtService;
    
    public async Task<Result<TokenResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Checking if user exists
        
        User? user = null;
        
        if (request.Username != null)
            user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
        else if (request.Email != null)
            user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        
        if (user == null) 
            return Result<TokenResponseDto>.Failure("Login attempt failed", ["Username or email is invalid."]);
        
        // Matching password hashes
        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        if (passwordResult != PasswordVerificationResult.Success)
            return Result<TokenResponseDto>.Failure("Login attempt failed", ["Invalid password."]);
        
        // Updating last login date
        user.LastLoginAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Generating JWT token
        var jwtToken = _jwtService.GenerateToken(user);
        
        // Generating refresh token
        var refreshToken = _jwtService.GenerateRefreshToken(user);
        
        // Saving refresh token to database
        await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        // Returning JWT token and refresh token (200)
        return Result<TokenResponseDto>.Success(new TokenResponseDto(jwtToken, refreshToken.Token), "User logged in successfully");
    }
}