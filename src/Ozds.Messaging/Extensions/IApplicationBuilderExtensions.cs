namespace Ozds.Messaging.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsMessaging(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
