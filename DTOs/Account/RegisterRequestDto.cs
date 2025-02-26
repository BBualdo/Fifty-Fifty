﻿namespace DTOs.Account;

public class RegisterRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}