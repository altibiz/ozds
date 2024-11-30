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
    services.AddBlazor(builder);
    services.AddMudBlazor();
    services.AddState();
    return services;
  }

  private static IServiceCollection AddBlazor(
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

    return services;
  }

  private static IServiceCollection AddMudBlazor(
    this IServiceCollection services
  )
  {
    services.AddMudServices();

    return services;
  }

  private static IServiceCollection AddState(
    this IServiceCollection services
  )
  {
    services.AddCascadingAuthenticationState();

    services.AddCascadingValue<CultureState>();
    services.AddCascadingValue<UserState>();
    services.AddCascadingValue<RepresentativeState>();
    services.AddCascadingValue<NotificationsState>();
    services.AddCascadingValue<AnalysisState>();
    services.AddCascadingValue<ThemeState>();
    services.AddCascadingValue<LayoutState>();

    return services;
  }

  private static IServiceCollection AddCascadingValue<T>(
    this IServiceCollection services)
  {
    return services.AddCascadingValue(_ => default(T));
  }
}
