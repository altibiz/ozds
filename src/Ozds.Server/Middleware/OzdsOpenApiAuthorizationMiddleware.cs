using Microsoft.AspNetCore.Authentication;
using Ozds.Users.Extensions;

namespace Ozds.Server.Middleware;

public class OzdsOpenApiAuthorizationMiddleware
{
  private readonly RequestDelegate next;

  public OzdsOpenApiAuthorizationMiddleware(RequestDelegate next)
  {
    this.next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Path.StartsWithSegments("/api")
      && (context.Request.Path.Value?.Contains("openapi") ?? false)
      && (!context.User.Identity?.IsAuthenticated ?? true))
    {
      await context.ChallengeAsync(
        HostExtensions.ChallengeScheme,
        new AuthenticationProperties
        {
          RedirectUri = context.Request.Path.ToString()
        });
      return;
    }

    await next(context);
  }
}
