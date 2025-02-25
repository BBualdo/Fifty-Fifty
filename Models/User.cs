﻿namespace Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public int Score { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<Household>? Households { get; set; }
    public ICollection<Invitation>? SentInvitations { get; set; }
    public ICollection<Invitation>? ReceivedInvitations { get; set; }
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}