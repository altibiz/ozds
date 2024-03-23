using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace Ozds.Business.Models;

public record UserModel(
  string Id,
  string UserName,
  string Email,
  List<string> Roles
);

public static class UserModelExtensions
{
  public static UserModel ToModel(this IUser abstractUser)
  {
    return abstractUser is User user
      ? new UserModel(
        user.UserId,
        user.UserName,
        user.Email,
        user.RoleNames.ToList()
      )
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
