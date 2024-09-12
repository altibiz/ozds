namespace Ozds.Iot.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsIot(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
