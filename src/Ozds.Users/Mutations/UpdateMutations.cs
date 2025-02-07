using Microsoft.AspNetCore.Identity;
using OrchardCore.Users.Models;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class UpdateMutations : IMutations
{
  private readonly UserManager<User> _userManager;

  public UpdateMutations(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  public async Task UpdateUser(
    string id,
    string userName,
    string userEmail,
    CancellationToken cancellationToken
  )
  {
    cancellationToken.ThrowIfCancellationRequested();

    var user = await _userManager.FindByIdAsync(id);
    if (user == null)
    {
      throw new InvalidOperationException($"User with id '{id}' not found.");
    }

    user.UserName = userName;
    user.Email = userEmail;

    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new InvalidOperationException(
        $"Failed to update Orchard user: {errors}"
      );
    }
  }
}
