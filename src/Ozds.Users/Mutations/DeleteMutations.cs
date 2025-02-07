using Microsoft.AspNetCore.Identity;
using OrchardCore.Users.Models;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class DeleteMutations : IMutations
{
  private readonly UserManager<User> _userManager;

  public DeleteMutations(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  public async Task DeleteUser(string id, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var user = await _userManager.FindByIdAsync(id);
    if (user == null)
    {
      throw new InvalidOperationException($"User with id '{id}' not found.");
    }

    var result = await _userManager.DeleteAsync(user);
    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new InvalidOperationException(
        $"Failed to delete Orchard user: {errors}"
      );
    }
  }
}
