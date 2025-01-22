using Altibiz.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.State;

namespace Ozds.Client.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsClient(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddState();
    services.AddModels();
    services.AddBlazor(builder);
    services.AddLocalStorage();
    services.AddMudBlazor();
    return services;
  }

  private static IServiceCollection AddState(
    this IServiceCollection services
  )
  {
    services.AddCascadingValue<CultureState>();
    services.AddCascadingValue<UserState>();
    services.AddCascadingValue<RepresentativeState>();
    services.AddCascadingValue<NotificationsState>();
    services.AddCascadingValue<AnalysisState>();
    services.AddCascadingValue<ThemeState>();
    services.AddCascadingValue<LayoutState>();

    return services;
  }

  private static IServiceCollection AddModels(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IModelComponentProvider));

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

    services.AddCascadingAuthenticationState();

    return services;
  }

  private static IServiceCollection AddMudBlazor(
    this IServiceCollection services
  )
  {
    services.AddMudServices();

    return services;
  }

  private static IServiceCollection AddLocalStorage(
    this IServiceCollection services
  )
  {
    services.AddBlazoredLocalStorage();

    return services;
  }

  private static IServiceCollection AddCascadingValue<T>(
    this IServiceCollection services)
  {
    return services.AddCascadingValue(_ => default(T));
  }
}
