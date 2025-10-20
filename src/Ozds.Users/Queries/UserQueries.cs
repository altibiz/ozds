using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Ozds.Users.Entities;
using Ozds.Users.Extensions;
using Ozds.Users.Options;
using Ozds.Users.Queries.Abstractions;

namespace Ozds.Users.Queries;

public class UserQueries(
  IServiceProvider serviceProvider,
  ILogger<UserQueries> logger,
  IOptions<OzdsUsersOptions> options
) : IQueries
{
  private readonly OzdsUsersParsedOidcConnectionString connectionString =
    new(options.Value.Oidc.ConnectionString);

  public string LoginHref
  {
    get { return connectionString.Authority; }
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

    return CreateUserEntityFromClaims(principal.Claims);
  }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

  public async Task<UserEntity?> ReadUserById(
    string id,
    CancellationToken cancellationToken)
  {
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var idFilter = $"{options.Value.Ldap.UserIdAttribute}={id}";
      var ouFilter = $"objectClass={options.Value.Ldap.UserFilterObjectClass}";
      var filter = $"(&({ouFilter})({idFilter}))";

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute
      };

      var searchResults = await Task.Run(
        () => ldapConnection.Search(
          options.Value.Ldap.BaseDn,
          LdapConnection.ScopeSub,
          filter,
          attributes,
          false
        ), cancellationToken);

      if (!searchResults.HasMore())
      {
        return null;
      }

      var entry = searchResults.Next();
      return CreateUserEntityFromLdapEntry(entry);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error");
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
    using var scope = serviceProvider.CreateAsyncScope();
    var ldapConnection = scope.ServiceProvider
      .GetRequiredService<LdapConnection>();

    try
    {
      var filter = $"(objectClass={options.Value.Ldap.UserFilterObjectClass})";
      if (!string.IsNullOrWhiteSpace(search))
      {
        search = search.EscapeLdap();
        filter =
          $"(&{filter}({options.Value.Ldap.UserNameAttribute}=*{search}*))";
      }

      string[] attributes =
      {
        options.Value.Ldap.UserIdAttribute,
        options.Value.Ldap.UserNameAttribute,
        options.Value.Ldap.UserEmailAttribute
      };

      var searchResults = await Task.Run(
        () => ldapConnection.Search(
          options.Value.Ldap.BaseDn,
          LdapConnection.ScopeSub,
          filter,
          attributes,
          false
        ), cancellationToken);

      var allEntries = new List<LdapEntry>();
      while (searchResults.HasMore()
        && !cancellationToken.IsCancellationRequested)
      {
        allEntries.Add(searchResults.Next());
      }

      var totalCount = allEntries.Count;

      var startIndex = pageNumber * pageSize;
      var pagedEntries = allEntries
        .Skip(startIndex)
        .Take(pageSize)
        .ToList();

      var users = pagedEntries
        .Select(CreateUserEntityFromLdapEntry)
        .ToList();

      return (users, totalCount);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "LDAP error");
      return (new List<UserEntity>(), 0);
    }
  }

  public async Task<string?> ReadAuthenticatedUserId(
    CancellationToken _
  )
  {
    var httpContextAccessor = serviceProvider
      .GetService<IHttpContextAccessor>();
    var user = httpContextAccessor?.HttpContext?.User;

    if (user is null)
    {
      try
      {
        var authenticationStateProvider = serviceProvider
          .GetService<AuthenticationStateProvider>();
        if (authenticationStateProvider is not null)
        {
          var authenticationState = await authenticationStateProvider
            .GetAuthenticationStateAsync();
          user = authenticationState.User;
        }
      }
      catch (Exception)
      {
        // NOTE: says not to do this outside component scope
      }
    }

    if (user is not null && user.Claims
        .FirstOrDefault(x => x.Type == options.Value.Oidc.UserIdClaim)?.Value
      is { } id)
    {
      return id;
    }

    return null;
  }

  private UserEntity CreateUserEntityFromClaims(
    IEnumerable<Claim> claims
  )
  {
    var id = claims
      .FirstOrDefault(claim => claim.Type == options.Value.Oidc.UserIdClaim)
      ?.Value;
    var email = claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
    var name = claims
      .FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

    return new UserEntity
    {
      Id = id ?? string.Empty,
      Email = email ?? string.Empty,
      Name = name ?? email ?? id ?? string.Empty
    };
  }

  private UserEntity CreateUserEntityFromLdapEntry(
    LdapEntry entry
  )
  {
    return new UserEntity
    {
      Id = GetAttributeValue(entry, options.Value.Ldap.UserIdAttribute)
        ?? string.Empty,
      Email = GetAttributeValue(entry, options.Value.Ldap.UserEmailAttribute)
        ?? string.Empty,
      Name = GetAttributeValue(entry, options.Value.Ldap.UserNameAttribute)
        ?? string.Empty
    };
  }

  private static string? GetAttributeValue(
    LdapEntry entry,
    string attributeName
  )
  {
    try
    {
      var attribute = entry.GetAttribute(attributeName);
      if (attribute != null && attribute.Size() > 0)
      {
        return attribute.StringValue;
      }
    }
    catch (Exception)
    {
      // Attribute doesn't exist
    }

    return null;
  }
}
