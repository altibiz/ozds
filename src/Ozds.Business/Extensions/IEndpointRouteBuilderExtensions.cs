using System.Reflection;

namespace Ozds.Business.Extensions;

public static class IEndpointRouteBuilderExtensions
{
  public static IEndpointRouteBuilder MapOzdsIot(
    this IEndpointRouteBuilder endpoints,
    string controller,
    string action,
    string prefix
  )
  {
    endpoints.MapAreaControllerRoute(
      "Ozds.Business",
      Assembly.GetCallingAssembly().GetName().Name
      ?? throw new InvalidOperationException("Assembly name not found"),
      prefix + "/push/{messengerId}",
      new { controller, action }
    );

    return endpoints;
  }
}
