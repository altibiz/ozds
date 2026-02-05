using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ozds.Business.Queries;

namespace Ozds.Business.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncAuthorizationFilter
{
  public const string PrincipalModelTypeClaim = "principal_model_type";

  public const string PrincipalModelIdClaim = "principal_model_id";

  public const string ApiKeyIdClaim = "api_key_id";

  public const string ApiKeyAuthItemKey = "api_key_auth";

  private const string ApiKeyHeaderScheme = "Bearer";

  public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
  {
    var clock =
      context.HttpContext.RequestServices.GetRequiredService<ClockQueries>();
    var now = clock.Now();

    var req = context.HttpContext.Request;
    var header = req.Headers.Authorization.ToString();

    if (
      string.IsNullOrWhiteSpace(header)
      || !header.StartsWith(ApiKeyHeaderScheme)
    )
    {
      context.Result = new ForbidResult();
      return;
    }

    var token = header[ApiKeyHeaderScheme.Length..].Trim();

    var manager =
      context.HttpContext.RequestServices.GetRequiredService<ApiKeyManager>();

    var (keyId, provided) = manager.Split(token);

    var apiKeyAuthCache =
      context.HttpContext.RequestServices.GetRequiredService<ApiKeyAuthQueries>();

    var auth = await apiKeyAuthCache.ReadByApiKeyIdAndScopeId(
      keyId,
      null,
      context.HttpContext.RequestAborted
    );

    if (auth is null)
    {
      context.Result = new ForbidResult();
      return;
    }

    if (auth.ApiKey.ExpiresOn is not null && auth.ApiKey.ExpiresOn < now)
    {
      context.Result = new ForbidResult();
      return;
    }

    var ok = manager.Verify(provided, auth.ApiKey.Hash);
    if (!ok)
    {
      context.Result = new ForbidResult();
      return;
    }

    var claims = new List<Claim>
    {
      new(PrincipalModelTypeClaim, auth.ApiKey.PrincipalModelType),
      new(PrincipalModelIdClaim, auth.ApiKey.PrincipalModelId),
      new(ApiKeyIdClaim, auth.ApiKey.Id),
    };
    var identity = new ClaimsIdentity(claims, "ApiKey");

    context.HttpContext.User = new ClaimsPrincipal(identity);

    context.HttpContext.Items[ApiKeyAuthItemKey] = auth;
  }
}
