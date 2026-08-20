using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Ozds.Users.Entities;
using Ozds.Users.Queries.Abstractions;

namespace Ozds.Users.Queries;

public class UserQueries(
  IServiceProvider serviceProvider,
  ILogger<UserQueries> logger,
  UserManager<OzdsUser> userManager
) : IQueries
{
  public string LoginHref
  {
    get { return "/app/auth/login"; }
  }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public async Task<UserEntity?> ReadUserByClaimsPrincipal(
    ClaimsPrincipal principal,
    CancellationToken cancellationToken
  )
  {
    if (principal.Identity?.IsAuthenticated != true)
    {
      return null;
    }

    return CreateUserEntityFromClaims(principal);
  }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

  public async Task<UserEntity?> ReadUserById(
    string id,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var user = await userManager.FindByIdAsync(id);
      if (user is null)
      {
        return null;
      }

      return CreateUserEntityFromOzdsUser(user);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error reading user by id");
      return null;
    }
  }

  public async Task<(List<UserEntity> Items, int TotalCount)> ReadUsers(
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken,
    string? search = null
  )
  {
    try
    {
      var query = userManager.Users;

      if (!string.IsNullOrWhiteSpace(search))
      {
        var searchLower = search.ToLowerInvariant();
        query = query.Where(u =>
          (u.UserName != null
            && u.UserName.ToLower().Contains(searchLower))
          || (u.Email != null && u.Email.ToLower().Contains(searchLower))
          || u.DisplayName.ToLower().Contains(searchLower)
        );
      }

      var totalCount = query.Count();

      var users = query
        .OrderBy(u => u.UserName)
        .Skip(pageNumber * pageSize)
        .Take(pageSize)
        .Select(u => new UserEntity
        {
          Id = u.Id,
          Email = u.Email ?? string.Empty,
          Name = u.DisplayName.Length > 0
            ? u.DisplayName
            : u.UserName ?? u.Email ?? u.Id,
        })
        .ToList();

      return (users, totalCount);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error reading users");
      return (new List<UserEntity>(), 0);
    }
  }

  public async Task<string?> ReadAuthenticatedUserId(CancellationToken _)
  {
    var httpContextAccessor =
      serviceProvider.GetService<IHttpContextAccessor>();
    var user = httpContextAccessor?.HttpContext?.User;

    if (user is null)
    {
      try
      {
        var authenticationStateProvider =
          serviceProvider.GetService<AuthenticationStateProvider>();
        if (authenticationStateProvider is not null)
        {
          var authenticationState =
            await authenticationStateProvider.GetAuthenticationStateAsync();
          user = authenticationState.User;
        }
      }
      catch (Exception)
      {
        // NOTE: says not to do this outside component scope
      }
    }

    if (
      user is not null
      && user.Identity?.IsAuthenticated == true
      && user
          .Claims.FirstOrDefault(x =>
            x.Type == ClaimTypes.NameIdentifier
          )
          ?.Value
        is { } id
    )
    {
      return id;
    }

    return null;
  }

  private static UserEntity CreateUserEntityFromClaims(
    ClaimsPrincipal principal
  )
  {
    var id = principal
      .Claims.FirstOrDefault(claim =>
        claim.Type == ClaimTypes.NameIdentifier
      )
      ?.Value;
    var email = principal
      .Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)
      ?.Value;
    var name = principal
      .Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)
      ?.Value;

    return new UserEntity
    {
      Id = id ?? string.Empty,
      Email = email ?? string.Empty,
      Name = name ?? email ?? id ?? string.Empty,
    };
  }

  private static UserEntity CreateUserEntityFromOzdsUser(OzdsUser user)
  {
    return new UserEntity
    {
      Id = user.Id,
      Email = user.Email ?? string.Empty,
      Name = user.DisplayName.Length > 0
        ? user.DisplayName
        : user.UserName ?? user.Email ?? user.Id,
    };
  }
}
