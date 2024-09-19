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
    // Blazor
    services.AddBlazor(builder);

    // MudBlazor
    services.AddMudBlazor();

    // State
    services.AddCascadingAuthenticationState();
    services.AddCascadingValue(_ => default(UserState));
    services.AddCascadingValue(_ => default(RepresentativeState));
    services.AddCascadingValue(_ => default(LogoutTokenState));
    services.AddCascadingValue(_ => new UserLayoutState(
      false,
      [],
      n => { },
      s => { },
      c => { }
    ));

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
}
