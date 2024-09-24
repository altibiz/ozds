namespace Ozds.Email.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsEmail(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
