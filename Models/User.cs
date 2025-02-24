using Microsoft.AspNetCore.Identity;

namespace Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Score { get; set; } = 0;
    public ICollection<Household>? Households { get; set; }
    public ICollection<Invitation>? SentInvitations { get; set; }
    public ICollection<Invitation>? ReceivedInvitations { get; set; }
}