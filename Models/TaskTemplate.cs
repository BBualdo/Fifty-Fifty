namespace Models;

public class TaskTemplate
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Score { get; set; }
}

public static class TaskTemplateExtensions
{
    public static Task ToTask(this TaskTemplate taskTemplate, string userId, Guid houseHoldId)
    {
        return new Task
        {
            Id = Guid.NewGuid(),
            Title = taskTemplate.Title,
            Score = taskTemplate.Score,
            AddedAt = DateOnly.FromDateTime(DateTime.Now),
            UserId = userId,
            HouseHoldId = houseHoldId
        };
    }
}