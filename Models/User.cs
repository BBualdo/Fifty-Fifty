using Microsoft.AspNetCore.Identity;

namespace Models;

public class User : IdentityUser
{
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
}