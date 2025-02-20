using System.ComponentModel.DataAnnotations;

namespace Models;

public class Household
{
    public Guid Id { get; set; }
    [StringLength(64)] public required string Name { get; set; }
}