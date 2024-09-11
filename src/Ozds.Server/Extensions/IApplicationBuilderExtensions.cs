using Microsoft.AspNetCore.Mvc;
using Ozds.Server.Controllers;

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
      "/iot/update/{id}",
      typeof(IotController),
      nameof(IotController.Update)
    );

    endpoints.MapOzdsServerRoute(
      "/iot/poll/{id}",
      typeof(IotController),
      nameof(IotController.Poll)
    );

    endpoints.MapOzdsServerRoute(
      "/",
      typeof(IndexController),
      nameof(IndexController.Index)
    );

    endpoints.MapOzdsServerRoute(
      "/app/{culture}/{**catchall}",
      typeof(AppController),
      nameof(AppController.Catchall)
    );

    endpoints.MapBlazorHub("/app/{culture}/_blazor");

    return app;
  }

  private static void MapOzdsServerRoute(
    this IEndpointRouteBuilder endpoints,
    string pattern,
    Type controller,
    string action)
  {
    endpoints.MapAreaControllerRoute(
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
