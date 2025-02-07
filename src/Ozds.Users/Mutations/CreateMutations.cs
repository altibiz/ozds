using Microsoft.AspNetCore.Identity;
using OrchardCore.Users.Models;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class CreateMutations : IMutations
{
  private readonly UserManager<User> _userManager;

  public CreateMutations(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  public async Task<User> CreateUser(
    string userName,
    string userEmail,
    CancellationToken cancellationToken
  )
  {
    cancellationToken.ThrowIfCancellationRequested();

    var user = new User { UserName = userName, Email = userEmail };

    var result = await _userManager.CreateAsync(user);
    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new InvalidOperationException(
        $"Failed to create Orchard user: {errors}"
      );
    }

    return user;
  }
}
