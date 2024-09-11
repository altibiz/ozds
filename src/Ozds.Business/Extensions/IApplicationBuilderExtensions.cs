namespace Ozds.Business.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsBusiness(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
