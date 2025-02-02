namespace Ozds.Jobs.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsJobs(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
