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
      "/download/location-energy-card/{culture}/{locationId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.LocationEnergyCard)
    );

    endpoints.MapOzdsServerRoute(
      "/download/network-user-energy-card/{culture}/{networkUserId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.NetworkUserEnergyCard)
    );

    endpoints.MapOzdsServerRoute(
      "/download/network-user-invoice/{id}",
      typeof(DownloadController),
      nameof(DownloadController.NetworkUserInvoice)
    );

    endpoints.MapOzdsServerRoute(
      "/download/network-user-invoice-preview/{networkUserId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.NetworkUserInvoicePreview)
    );

    endpoints.MapOzdsServerRoute(
      "/download/measurement-location-accounting-period/{culture}/{measurementLocationId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.MeasurementLocationAccountingPeriod)
    );

    endpoints.MapOzdsServerRoute(
      "/download/measurement-location-load-curve/{culture}/{measurementLocationId}/{obisString}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.MeasurementLocationLoadCurve)
    );

    endpoints.MapOzdsServerRoute(
      "/download/meter-accounting-period/{culture}/{meterId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.MeterAccountingPeriod)
    );

    endpoints.MapOzdsServerRoute(
      "/download/meter-load-curve/{culture}/{meterId}/{obisString}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.MeterLoadCurve)
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
        controller = controller.Name[..^nameof(Controller).Length],
        action
      }
    );
  }
}
