using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users.Models;
using Ozds.Data.Models;

namespace Ozds.Data;

public partial class OzdsDbClient
{
  public async Task<UserModel?> ReadUserFromClaimsPrincipal(
    ClaimsPrincipal principal)
  {
    return await _userManager.GetUserAsync(principal) is { } user
      ? UserToModel(user)
      : null;
  }

  public async Task<UserModel?> ReadUser(string userId)
  {
    return await _userManager.FindByIdAsync(userId) is { } user
      ? UserToModel(user)
      : null;
  }

  public async Task<List<MaybeRepresentingUserModel>?> ReadUserRepresentatives(
    int skip = 0, int take = TakeLimit)
  {
    var users = await _userManager.Users
      .Skip(skip)
      .Take(take)
      .ToListAsync();

    var userIds = users.Select(user => user.UserId).ToList();

    var representatives = await _context.Representatives
      .Where(entity => userIds.Contains(entity.UserId))
      .ToListAsync();

    return users.Select(user => new MaybeRepresentingUserModel(
      UserToModel(user),
      representatives.Find(representative =>
        representative.UserId == user.UserId) is { } representative
        ? RepresentativeToModel(representative)
        : null
    )).ToList();
  }

  public async Task<RepresentativeModel?> ReadRepresentativeByUser(
    string userId)
  {
    return await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.UserId == userId) is { } entity
      ? RepresentativeToModel(entity)
      : null;
  }

  public async Task<RepresentingUserModel?> ReadRepresentingUserByUser(
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

  public async Task<MaybeRepresentingUserModel?>
    ReadMaybeRepresentingUserByUser(string userId)
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

  public async Task<RepresentingUserModel?>
    ReadUserRepresentingUserByRepresentative(string id)
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

  private static UserModel UserToModel(User user)
  {
    return new UserModel(
      user.UserId,
      user.UserName,
      user.Email,
      user.RoleNames.ToList()
    );
  }
}
