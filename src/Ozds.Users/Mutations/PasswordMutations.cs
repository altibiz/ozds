using Microsoft.AspNetCore.Identity;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class PasswordMutations(
  ILogger<PasswordMutations> logger,
  UserManager<OzdsUser> userManager
) : IMutations
{
  public async Task Update(
    PasswordEntity entity,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var user = await userManager.FindByIdAsync(entity.UserId);
      if (user is null)
      {
        throw new InvalidOperationException(
          $"User with id '{entity.UserId}' not found"
        );
      }

      var result = await userManager.ChangePasswordAsync(
        user,
        entity.OldPassword,
        entity.NewPassword
      );

      if (!result.Succeeded)
      {
        var errors = string.Join(
          ", ",
          result.Errors.Select(e => e.Description)
        );
        throw new InvalidOperationException(
          $"Failed to change password: {errors}"
        );
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error during password update");
    }
  }
}
