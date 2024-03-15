using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using Ozds.Data.Models;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  public async Task<List<MaybeRepresentingUserModel>?> ReadMaybeRepresentingUsers(
    int skip = 0, int take = TakeLimit)
  {
    var users = await _userManager.Users
      .Skip(skip)
      .Take(take)
      .ToListAsync();

    var userIds = users.Select(user => GetUserId(user)).ToList();

    var representatives = await _context.Representatives
      .Where(entity => userIds.Contains(entity.UserId))
      .ToListAsync();

    return users.Select(user => new MaybeRepresentingUserModel(
      UserToModel(user),
      representatives.Find(representative =>
        representative.UserId == GetUserId(user)) is { } representative
        ? RepresentativeToModel(representative)
        : null
    )).ToList();
  }

  public async Task<UserModel?> ReadUserByClaimsPrincipal(
    ClaimsPrincipal principal)
  {
    return await _userManager.GetUserAsync(principal) is { } user
      ? UserToModel(user)
      : null;
  }

  public async Task<UserModel?> ReadUserByUserId(string userId)
  {
    return await _userManager.FindByIdAsync(userId) is { } user
      ? UserToModel(user)
      : null;
  }

  public async Task<RepresentativeModel?> ReadRepresentativeByUserId(
    string userId)
  {
    return await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.UserId == userId) is { } entity
      ? RepresentativeToModel(entity)
      : null;
  }

  public async Task<RepresentingUserModel?>
    ReadRepresentingUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
  {
    var user = await _userManager.GetUserAsync(claimsPrincipal);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == GetUserId(user));
    if (representative is null)
    {
      return null;
    }

    return new RepresentingUserModel(
      UserToModel(user),
      RepresentativeToModel(representative)
    );
  }

  public async Task<RepresentingUserModel?> ReadRepresentingUserByUserId(
    string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == userId);
    if (representative is null)
    {
      return null;
    }

    return new RepresentingUserModel(UserToModel(user),
      RepresentativeToModel(representative));
  }

  public async Task<RepresentingUserModel?>
    ReadRepresentingUserByRepresentativeId(string id)
  {
    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.Id == id);
    if (representative is null)
    {
      return null;
    }

    var user = await _userManager.FindByIdAsync(representative.UserId);
    if (user is null)
    {
      return null;
    }

    return new RepresentingUserModel(UserToModel(user),
      RepresentativeToModel(representative));
  }

  public async Task<MaybeRepresentingUserModel?>
    ReadMaybeRepresentingUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
  {
    var user = await _userManager.GetUserAsync(claimsPrincipal);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == GetUserId(user));
    if (representative is null)
    {
      return new MaybeRepresentingUserModel(UserToModel(user), null);
    }

    return new MaybeRepresentingUserModel(UserToModel(user),
      RepresentativeToModel(representative));
  }

  public async Task<MaybeRepresentingUserModel?>
    ReadMaybeRepresentingUserByUserId(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == userId);
    if (representative is null)
    {
      return new MaybeRepresentingUserModel(UserToModel(user), null);
    }

    return new MaybeRepresentingUserModel(UserToModel(user),
      RepresentativeToModel(representative));
  }

  private static UserModel UserToModel(IUser abstractUser)
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

  private static string GetUserId(IUser abstractUser)
  {
    return abstractUser is User user
      ? user.UserId
      : throw new InvalidOperationException(
        "User is not an Orchard Core user"
      );
  }
}
