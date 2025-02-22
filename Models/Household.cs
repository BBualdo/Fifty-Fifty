using System.ComponentModel.DataAnnotations;

namespace Models;

public class Household
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}