using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using YesSql;

namespace Ozds.Business.Queries;

public partial class OzdsRelationalQueries
{
  public async Task<UserModel?> UserByClaimsPrincipal(
    ClaimsPrincipal principal)
  {
    return await _userManager.GetUserAsync(principal) is { } user
      ? user.ToModel()
      : null;
  }

  public async Task<UserModel?> UserByUserId(string userId)
  {
    return await _userManager.FindByIdAsync(userId) is { } user
      ? user.ToModel()
      : null;
  }

  public async Task<RepresentativeModel?> RepresentativeByUserId(
    string userId)
  {
    return await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.UserId == userId) is { } entity
      ? entity.ToModel()
      : null;
  }

  public async Task<PaginatedList<MaybeRepresentingUserModel>>
    MaybeRepresentingUsers(
      Expression<Func<UserIndex, bool>>? filter = null,
      int pageNumber = QueryConstants.StartingPage,
      int pageCount = QueryConstants.DefaultPageCount)
  {
    var users = await _session
      .Query<User, UserIndex>()
      .QueryPaged(
        UserModelExtensions.ToModel,
        filter,
        pageNumber,
        pageCount
      );
    var userIds = users.Items
      .Select(user => user.Id)
      .ToList();

    var representatives = await _context.Representatives
      .Where(entity => userIds.Contains(entity.UserId))
      .ToListAsync();

    return users.Items
      .Select(user => new MaybeRepresentingUserModel(
        user,
        representatives.Find(representative =>
          representative.UserId == user.Id) is { } representative
          ? representative.ToModel()
          : null
      ))
      .ToPaginatedList(users.TotalCount);
  }

  public async Task<PaginatedList<RepresentingUserModel>> RepresentingUsers(
    Expression<Func<UserIndex, bool>>? filter = null,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount)
  {
    var users = await _session
      .Query<User, UserIndex>()
      .QueryPaged(
        UserModelExtensions.ToModel,
        filter,
        pageNumber,
        pageCount
      );
    var userIds = users.Items
      .Select(user => user.Id)
      .ToList();

    var representatives = await _context.Representatives
      .Where(entity => userIds.Contains(entity.UserId))
      .ToListAsync();

    return users.Items
      .Select(user => new MaybeRepresentingUserModel(
        user,
        representatives.Find(representative =>
          representative.UserId == user.Id) is { } representative
          ? representative.ToModel()
          : null
      ))
      .Where(maybeRepresentingUser =>
        maybeRepresentingUser.Representative is not null)
      .Select(maybeRepresentingUser => new RepresentingUserModel(
        maybeRepresentingUser.User,
        maybeRepresentingUser.Representative!
      ))
      .ToPaginatedList(users.TotalCount);
  }

  public async Task<RepresentingUserModel?>
    RepresentingUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
  {
    var user = await _userManager.GetUserAsync(claimsPrincipal);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == user.GetUserId());
    if (representative is null)
    {
      return null;
    }

    return new RepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<RepresentingUserModel?> RepresentingUserByUserId(
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

    return new RepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<RepresentingUserModel?>
    RepresentingUserByRepresentativeId(string id)
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

    return new RepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<MaybeRepresentingUserModel?>
    MaybeRepresentingUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
  {
    var user = await _userManager.GetUserAsync(claimsPrincipal);
    if (user is null)
    {
      return null;
    }

    var representative =
      await _context.Representatives.FirstOrDefaultAsync(entity =>
        entity.UserId == user.GetUserId());
    if (representative is null)
    {
      return new MaybeRepresentingUserModel(user.ToModel(), null);
    }

    return new MaybeRepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }

  public async Task<MaybeRepresentingUserModel?>
    MaybeRepresentingUserByUserId(string userId)
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
      return new MaybeRepresentingUserModel(user.ToModel(), null);
    }

    return new MaybeRepresentingUserModel(
      user.ToModel(),
      representative.ToModel()
    );
  }
}
