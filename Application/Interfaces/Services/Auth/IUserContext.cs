namespace Application.Interfaces.Services.Auth;

public interface IUserContext
{
    Guid? UserId { get; }
}