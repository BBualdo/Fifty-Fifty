namespace Models;

public class TaskTemplate
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public int Score { get; set; }
}