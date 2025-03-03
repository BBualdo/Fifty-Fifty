namespace Domain.Entities;

public class Invitation
{
    public Guid Id { get; set; }
    public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(3);

    public Guid InvitedUserId { get; set; }
    public User InvitedUser { get; set; } = null!;

    public Guid HouseHoldId { get; set; }
    public Household Household { get; set; } = null!;

    public Guid InvitingUserId { get; set; }
    public User InvitingUser { get; set; } = null!;
} 