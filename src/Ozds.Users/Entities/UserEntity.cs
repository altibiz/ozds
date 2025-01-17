using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace Ozds.Users.Entities;

public class UserEntity
{
  public string Id { get; set; } = default!;

  public string UserName { get; set; } = default!;

  public string Email { get; set; } = default!;
}

public static class UserModelExtensions
{
  public static UserEntity ToEntity(this IUser abstractUser)
  {
    return abstractUser is User user
      ? new()
      {
        Id = user.UserId,
        UserName = user.UserName,
        Email = user.Email
      }
      : throw new InvalidOperationException(
        "User is not an Orchard Core user"
      );
  }

  public static string GetId(this IUser abstractUser)
  {
    return abstractUser is User user
      ? user.UserId
      : throw new InvalidOperationException(
        "User is not an Orchard Core user"
      );
  }
}
