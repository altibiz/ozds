using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ozds.Client.Extensions;

public static class IEndpointRouteBuilderExtensions
{
  public static IEndpointRouteBuilder MapOzdsClient(
    this IEndpointRouteBuilder endpoints,
    string controller,
    string action,
    string prefix
  )
  {
    endpoints.MapAreaControllerRoute(
      "Ozds.Client",
      Assembly.GetCallingAssembly().GetName().Name
      ?? throw new InvalidOperationException("Assembly name not found"),
      prefix + "/{**catchall}",
      new { controller, action }
    );
    endpoints.MapBlazorHub(prefix + "/_blazor");

    return endpoints;
  }
}
