using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
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
  IServiceProvider serviceProvider,
  ISession session
) : IUserQueries
{
  public async Task<UserEntity?> ReadUserByClaimsPrincipal(
    ClaimsPrincipal principal,
    CancellationToken cancellationToken
  )
  {
    return await userManager.GetUserAsync(principal) is { } user
      ? user.ToEntity()
      : null;
  }

  public async Task<UserEntity?> ReadUserById(
    string id,
    CancellationToken cancellationToken)
  {
    return await session
      .Query<User, UserIndex>()
      .Where(index => index.UserId == id)
      .FirstOrDefaultAsync() is { } user
      ? user.ToEntity()
      : null;
  }

  public async Task<(List<UserEntity> Items, int TotalCount)> ReadUsers(
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
      .Skip(pageNumber * pageSize)
      .Take(pageSize);

    var users = await filtered.ListAsync();

    return (users.Select(user => user.ToEntity()).ToList(), count);
  }

  public async Task<string?> ReadAuthenticatedUserId(
    CancellationToken cancellationToken
  )
  {
    var httpContextAccessor = serviceProvider
      .GetService<IHttpContextAccessor>();
    if (httpContextAccessor?.HttpContext?.User.Claims
        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
      is { } mvcUserId)
    {
      return mvcUserId;
    }

    try
    {
      var authenticationState = serviceProvider
        .GetService<AuthenticationStateProvider>();
      if (authenticationState is { } authenticationStateProvider
        && (await authenticationStateProvider
          .GetAuthenticationStateAsync()).User.Claims
        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value
        is { } fluxUserId)
      {
        return fluxUserId;
      }
    }
    catch (Exception)
    {
      // NOTE: throws saying you can't call it outside of component DI container
    }

    return null;
  }
}
