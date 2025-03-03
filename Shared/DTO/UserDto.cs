namespace Shared.DTO;

public class UserDto
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string? LastName { get; private set; }
    public int Score { get; private set; }
    public string Role { get; private set; }
    

    public UserDto(Guid id, string username, string email, string firstName, string? lastName, int score, string role)
    {
        Id = id;
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Score = score;
        Role = role;
    }
}