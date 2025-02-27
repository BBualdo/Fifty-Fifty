namespace Models;

public class Household
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = [];
    public ICollection<Task> Tasks { get; set; } = [];
}