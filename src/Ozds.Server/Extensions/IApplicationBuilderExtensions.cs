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
      "/download/network-user-monthly-aggregates/{networkUserId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.NetworkUserMonthlyAggregates)
    );

    endpoints.MapOzdsServerRoute(
      "/download/location-monthly-aggregates/{locationId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.LocationMonthlyAggregates)
    );

    endpoints.MapOzdsServerRoute(
      "/download/meter-quarter-hourly-aggregates-for-month/{meterId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(DownloadController.MeterQuarterHourlyAggregatesForMonth)
    );

    endpoints.MapOzdsServerRoute(
      "/download/measurement-location-quarter-hourly-aggregates-for-month/{measurementLocationId}/{year:int}/{month:int}",
      typeof(DownloadController),
      nameof(
        DownloadController.MeasurementLocationQuarterHourlyAggregatesForMonth
      )
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
