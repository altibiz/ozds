using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Ozds.Client.State;

namespace Ozds.Client.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsClient(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services
      .AddRazorComponents()
      .AddInteractiveServerComponents();

    services
      .AddServerSideBlazor()
      .AddCircuitOptions(
        options =>
        {
          if (builder.Environment.IsDevelopment())
          {
            options.DetailedErrors = true;
          }
        })
      .AddHubOptions(
        options =>
        {
          if (builder.Environment.IsDevelopment())
          {
            options.EnableDetailedErrors = true;
          }
        });

    services.AddMudServices();

    services.AddCascadingAuthenticationState();
    services.AddCascadingValue(_ => default(UserState));
    services.AddCascadingValue(_ => default(RepresentativeState));

    return services;
  }

  public static IApplicationBuilder UseOzdsClient(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    endpoints.MapControllerRoute(
      "Ozds.Client.Controllers.Index.Index",
      "Ozds.Client",
      "/",
      new { controller = "Index", action = "Index" }
    );

    endpoints.MapAreaControllerRoute(
      "Ozds.Client.Controllers.App.Catchall",
      "Ozds.Client",
      "/app/{culture}/{**catchall}",
      new { controller = "App", action = "Catchall" }
    );

    endpoints.MapBlazorHub("/app/{culture}/_blazor");

    return app;
  }
}
