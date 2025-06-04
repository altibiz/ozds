using Microsoft.AspNetCore.Mvc;
using Ozds.Server.Controllers;
using Ozds.Server.Middleware;

namespace Ozds.Server.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsServer(
    this IHostApplicationBuilder builder
  )
  {
    if (builder is WebApplicationBuilder webBuilder)
    {
      webBuilder.WebHost.ConfigureKestrel(
        serverOptions =>
        {
          serverOptions.Limits.MinRequestBodyDataRate = null;
        });
    }

    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();
    builder.Services.AddServerSideBlazor();
    return builder;
  }

  public static WebApplication UseOzdsServer(
    this WebApplication app
  )
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
      app.UseHttpsRedirection();
    }

    app.UseMiddleware<OzdsExceptionMiddleware>();

    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapOzdsServerRoute(
      "/auth/login",
      typeof(AuthController),
      nameof(AuthController.Login)
    );

    app.MapOzdsServerRoute(
      "/auth/logout",
      typeof(AuthController),
      nameof(AuthController.Logout)
    );

    app.MapOzdsServerRoute(
      "/iot/push/{id}",
      typeof(IotController),
      nameof(IotController.Push)
    );

    app.MapOzdsServerRoute(
      "/document/network-user-invoice/{id}",
      typeof(DocumentController),
      nameof(DocumentController.NetworkUserInvoice)
    );

    app.MapOzdsServerRoute(
      "/document/network-user-invoice-preview/{networkUserId}/{year:int}/{month:int}",
      typeof(DocumentController),
      nameof(DocumentController.NetworkUserInvoicePreview)
    );

    app.MapOzdsServerRoute(
      "/report/location-energy-card/{culture}/{locationId}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.LocationEnergyCard)
    );

    app.MapOzdsServerRoute(
      "/report/network-user-energy-card/{culture}/{networkUserId}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.NetworkUserEnergyCard)
    );

    app.MapOzdsServerRoute(
      "/report/measurement-location-accounting-period/{culture}/{measurementLocationId}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.MeasurementLocationAccountingPeriod)
    );

    app.MapOzdsServerRoute(
      "/report/measurement-location-load-curve/{culture}/{measurementLocationId}/{obisString}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.MeasurementLocationLoadCurve)
    );

    app.MapOzdsServerRoute(
      "/report/meter-accounting-period/{culture}/{meterId}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.MeterAccountingPeriod)
    );

    app.MapOzdsServerRoute(
      "/report/meter-load-curve/{culture}/{meterId}/{obisString}/{year:int}/{month:int}",
      typeof(ReportController),
      nameof(ReportController.MeterLoadCurve)
    );

    app.MapOzdsServerRoute(
      "/data/measurements-by-meter/{meterId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.MeasurementsByMeter)
    );

    app.MapOzdsServerRoute(
      "/data/measurements-by-measurement-location/{measurementLocationId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.MeasurementsByMeasurementLocation)
    );

    app.MapOzdsServerRoute(
      "/data/quarter-hourly-aggregates-by-meter/{meterId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.QuarterHourlyAggregatesByMeter)
    );

    app.MapOzdsServerRoute(
      "/data/quarter-hourly-aggregates-by-measurement-location/{measurementLocationId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.QuarterHourlyAggregatesByMeasurementLocation)
    );

    app.MapOzdsServerRoute(
      "/data/daily-aggregates-by-meter/{meterId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.DailyAggregatesByMeter)
    );

    app.MapOzdsServerRoute(
      "/data/daily-aggregates-by-measurement-location/{measurementLocationId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.DailyAggregatesByMeasurementLocation)
    );

    app.MapOzdsServerRoute(
      "/data/monthly-aggregates-by-meter/{meterId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.MonthlyAggregatesByMeter)
    );

    app.MapOzdsServerRoute(
      "/data/monthly-aggregates-by-measurement-location/{measurementLocationId}/{fromDate}/{toDate}/{pageNumber:int}",
      typeof(DataController),
      nameof(DataController.MonthlyAggregatesByMeasurementLocation)
    );

    app.MapOzdsServerRoute(
      "/",
      typeof(ClientController),
      nameof(ClientController.Uncultured)
    );

    app.MapOzdsServerRoute(
      "/app",
      typeof(ClientController),
      nameof(ClientController.Uncultured)
    );

    app.MapBlazorHub("/app/{culture}/_blazor");

    app.MapOzdsServerRoute(
      "/app/{culture}/{**catchall}",
      typeof(ClientController),
      nameof(ClientController.Cultured)
    );

    return app;
  }

  private static ControllerActionEndpointConventionBuilder MapOzdsServerRoute(
    this WebApplication app,
    string pattern,
    Type controller,
    string action)
  {
    return app.MapAreaControllerRoute(
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
