namespace Ozds.Document.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsDocument(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
