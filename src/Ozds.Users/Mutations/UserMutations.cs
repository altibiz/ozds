using Microsoft.AspNetCore.Identity;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;

namespace Ozds.Users.Mutations;

public class UserMutations(
  ILogger<UserMutations> logger,
  UserManager<OzdsUser> userManager
) : IMutations
{
  public async Task Create(
    UserEntity entity,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var existingUser = await userManager.FindByIdAsync(entity.Id);
      if (existingUser is not null)
      {
        throw new InvalidOperationException(
          $"User with id '{entity.Id}' already exists"
        );
      }

      var user = new OzdsUser
      {
        Id = entity.Id,
        UserName = entity.Id,
        Email = entity.Email,
        DisplayName = entity.Name,
      };

      var result = await userManager.CreateAsync(user);
      if (!result.Succeeded)
      {
        var errors = string.Join(
          ", ",
          result.Errors.Select(e => e.Description)
        );
        throw new InvalidOperationException(
          $"Failed to create user: {errors}"
        );
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error during user creation");
    }
  }

  public async Task Update(
    UserEntity entity,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var user = await userManager.FindByIdAsync(entity.Id);
      if (user is null)
      {
        throw new InvalidOperationException(
          $"User with id '{entity.Id}' not found"
        );
      }

      user.Email = entity.Email;
      user.DisplayName = entity.Name;

      var result = await userManager.UpdateAsync(user);
      if (!result.Succeeded)
      {
        var errors = string.Join(
          ", ",
          result.Errors.Select(e => e.Description)
        );
        throw new InvalidOperationException(
          $"Failed to update user: {errors}"
        );
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error during user update");
    }
  }

  public async Task Delete(string id, CancellationToken cancellationToken)
  {
    try
    {
      var user = await userManager.FindByIdAsync(id);
      if (user is null)
      {
        throw new InvalidOperationException(
          $"User with id '{id}' not found"
        );
      }

      var result = await userManager.DeleteAsync(user);
      if (!result.Succeeded)
      {
        var errors = string.Join(
          ", ",
          result.Errors.Select(e => e.Description)
        );
        throw new InvalidOperationException(
          $"Failed to delete user: {errors}"
        );
      }
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error during user deletion");
    }
  }
}
