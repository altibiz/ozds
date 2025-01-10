using Microsoft.AspNetCore.Mvc;
using Ozds.Server.Controllers;
using Ozds.Server.Middleware;

namespace Ozds.Server.Extensions;

public static class IApplicationBuilderExtensions
{
  public static IApplicationBuilder UseOzdsServer(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    endpoints.MapOzdsServerRoute(
      "/iot/push/{id}",
      typeof(IotController),
      nameof(IotController.Push)
    );

    endpoints.MapOzdsServerRoute(
      "/",
      typeof(AppController),
      nameof(AppController.Uncultured)
    );

    endpoints.MapOzdsServerRoute(
      "/app",
      typeof(AppController),
      nameof(AppController.Uncultured)
    );

    endpoints.MapBlazorHub("/app/{culture}/_blazor");

    endpoints.MapOzdsServerRoute(
      "/app/{culture}/{**catchall}",
      typeof(AppController),
      nameof(AppController.Cultured)
    );

    app.UseMiddleware<ExceptionMiddleware>();

    return app;
  }

  private static ControllerActionEndpointConventionBuilder MapOzdsServerRoute(
    this IEndpointRouteBuilder endpoints,
    string pattern,
    Type controller,
    string action)
  {
    return endpoints.MapAreaControllerRoute(
      areaName: $"{nameof(Ozds)}.{nameof(Server)}",
      name: $"{controller.Namespace}.{controller.Name}.{action}",
      pattern: pattern,
      defaults: new
      {
        controller = controller.Name.Remove(
          controller.Name.Length -
          nameof(Controller).Length,
          nameof(Controller).Length),
        action
      }
    );
  }
}
