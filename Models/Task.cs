using System.ComponentModel.DataAnnotations;

namespace Models;

public class Task
{
    public Guid Id { get; set; }
    [StringLength(64)] public required string Title { get; set; }
    public DateOnly Date { get; set; }
    public int Score { get; set; }
    public required Guid UserId { get; set; }
    public User? User { get; set; }
    public required Guid HouseHoldId { get; set; }
    public Household? Household { get; set; }
}