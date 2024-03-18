using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ozds.Client.Extensions;

public static class IEndpointRouteBuilderExtensions
{
  public static IEndpointRouteBuilder MapOzdsClient(
    this IEndpointRouteBuilder endpoints,
    string controller,
    string action,
    string prefix = "/app"
  )
  {
    endpoints.MapBlazorHub(prefix + "/_blazor");
    endpoints.MapAreaControllerRoute(
      name: "Ozds.Client",
      areaName: "Ozds.Client",
      pattern: prefix + "/{**catchall}",
      defaults: new { controller, action }
    );

    return endpoints;
  }
}
