namespace Models;

public class Invitation
{
    public Guid Id { get; set; }
    public DateTime ExpirationDate { get; set; }
    public required string InvitedUserId { get; set; }
    public User? InvitedUser { get; set; }
    public Guid HouseHoldId { get; set; }
    public Household? Household { get; set; }
    public required string InvitingUserId { get; set; }
    public User? InvitingUser { get; set; }
}