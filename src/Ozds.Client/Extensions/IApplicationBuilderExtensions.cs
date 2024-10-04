using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ozds.Client.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsClient(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
