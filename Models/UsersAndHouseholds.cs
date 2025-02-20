namespace Models;

public class UsersAndHouseholds
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid HouseholdId { get; set; }
    public Household? Household { get; set; }
}