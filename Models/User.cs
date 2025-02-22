using Microsoft.AspNetCore.Identity;

namespace Models;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public int Score { get; set; }
    public ICollection<Household>? Households { get; set; }
    public ICollection<Invitation>? SentInvitations { get; set; }
    public ICollection<Invitation>? ReceivedInvitations { get; set; }
}