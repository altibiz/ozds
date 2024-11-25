using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Users;
using OrchardCore.Users.Indexes;
using OrchardCore.Users.Models;
using Ozds.Users.Entities;
using Ozds.Users.Queries.Abstractions;
using YesSql;
using ISession = YesSql.ISession;

namespace Ozds.Users.Queries;

public class UserQueries(
  UserManager<IUser> userManager,
  ISession session
) : IUserQueries
{
  public async Task<UserEntity?> UserByClaimsPrincipal(
    ClaimsPrincipal principal,
    CancellationToken cancellationToken
  )
  {
    return await userManager.GetUserAsync(principal) is { } user
      ? user.ToEntity()
      : null;
  }

  public async Task<UserEntity?> UserById(string id, CancellationToken cancellationToken)
  {
    return await session
      .Query<User, UserIndex>()
      .Where(index => index.UserId == id)
      .FirstOrDefaultAsync() is { } user
      ? user.ToEntity()
      : null;
  }

  public async Task<(List<UserEntity> Items, int TotalCount)> Users(
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken
  )
  {
    var ordered = session
      .Query<User, UserIndex>()
      .OrderBy(index => index.DocumentId);

    var count = await ordered.CountAsync();
    var filtered = ordered
      .Skip((pageNumber - 1) * pageSize)
      .Take(pageSize);

    var users = await filtered.ListAsync();

    return (users.Select(user => user.ToEntity()).ToList(), count);
  }
}
