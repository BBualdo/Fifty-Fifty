using System.ComponentModel.DataAnnotations;

namespace Models;

public class Task
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateOnly AddedAt { get; set; }

    [Range(1, 50)] 
    public int Score { get; set; }

    public bool IsCompleted { get; set; } = false;
    public DateOnly? CompletedAt { get; set; }
    public string? UserId { get; set; }
    public User? AssignedUser { get; set; }
    public required Guid HouseHoldId { get; set; }
    public Household? Household { get; set; }
}