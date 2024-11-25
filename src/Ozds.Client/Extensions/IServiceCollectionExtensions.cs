using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
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

    services.AddCascadingValue(_ => default(UserState));
    services.AddCascadingValue(_ => default(RepresentativeState));
    services.AddCascadingValue(
      _ => new LayoutState(
        false,
        false,
        false,
        _ => { },
        _ => { },
        _ => { },
        Breakpoint.None,
        _ => { }
      ));
    services.AddCascadingValue(
      _ => new ThemeState(
        ThemeState.Default(),
        false,
        _ => { },
        _ => { }
      ));
    services.AddCascadingValue(_ => new LogoutTokenState(string.Empty));

    return services;
  }
}
