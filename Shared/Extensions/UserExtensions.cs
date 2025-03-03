using Domain.Entities;
using Shared.DTO;

namespace Shared.Extensions;

public static class UserExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto(user.Id, user.Username, user.Email, user.FirstName, user.LastName, user.Score, user.Role.ToString());
    }
}