// TODO: validation in Ozds.Business

using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class UserMutations : IUserMutations
{
  private readonly UserManager<IUser> _userManager;

  public UserMutations(UserManager<IUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task<User> CreateUser(
    UserEntity userEntity,
    CancellationToken cancellationToken
  )
  {
    cancellationToken.ThrowIfCancellationRequested();

    var user = new User
    {
      UserName = userEntity.UserName,
      Email = userEntity.Email
    };

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

  public async Task UpdateUser(
    UserEntity userEntity,
    CancellationToken cancellationToken
  )
  {
    cancellationToken.ThrowIfCancellationRequested();

    var user = await _userManager.FindByIdAsync(userEntity.Id);
    if (user == null)
    {
      var message = $"User with id '{userEntity.Id}' not found.";
      throw new InvalidOperationException(message);
    }

    if (user is User concreteUser)
    {
      concreteUser.UserName = userEntity.UserName;
      concreteUser.Email = userEntity.Email;
    }

    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new InvalidOperationException(
        $"Failed to update Orchard user: {errors}"
      );
    }
  }

  public async Task DeleteUser(long id, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var result = await _userManager.DeleteAsync(new User { Id = id });
    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      throw new InvalidOperationException(
        $"Failed to delete Orchard user: {errors}"
      );
    }
  }
}
