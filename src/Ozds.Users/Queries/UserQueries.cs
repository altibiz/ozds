using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using Ozds.Users.Models;
using Ozds.Users.Queries.Abstractions;
using YesSql;
using ISession = YesSql.ISession;

namespace Ozds.Users.Queries;

public class UserQueries(
  UserManager<IUser> userManager,
  ISession session
) : IUserQueries
{
  public async Task<UserModel?> UserByClaimsPrincipal(
    ClaimsPrincipal principal)
  {
    return await userManager.GetUserAsync(principal) is { } user
      ? user.ToModel()
      : null;
  }

  public async Task<UserModel?> UserByUserId(string userId)
  {
    return await userManager.FindByIdAsync(userId) is { } user
      ? user.ToModel()
      : null;
  }

  public async Task<(List<UserModel> Items, int TotalCount)> Users(int pageNumber, int pageSize)
  {
    var ordered = session
      .Query<User, UserIndex>()
      .OrderBy(index => index.DocumentId);

    var count = await ordered.CountAsync();
    var filtered = ordered
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize);

    var users = await filtered.ListAsync();

    return (users.Select(user => user.ToModel()).ToList(), count);
  }
}
