namespace Models;

public class Invitation
{
    public Guid Id { get; set; }
    public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(3);

    public string? InvitedUserId { get; set; }
    public User? InvitedUser { get; set; }

    public Guid HouseHoldId { get; set; }
    public Household? Household { get; set; }

    public string? InvitingUserId { get; set; }
    public User? InvitingUser { get; set; }
}