namespace Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateOnly AddedAt { get; set; }
    public int Score { get; set; }

    public bool IsCompleted { get; set; } = false;
    public DateOnly? CompletedAt { get; set; }
    public Guid? UserId { get; set; }
    public User? AssignedUser { get; set; }
    public Guid HouseHoldId { get; set; }
    public Household Household { get; set; } = null!;
}