namespace Ozds.Data.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsData(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
