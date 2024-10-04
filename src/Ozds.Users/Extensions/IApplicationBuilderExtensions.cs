namespace Ozds.Users.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsUsers(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
