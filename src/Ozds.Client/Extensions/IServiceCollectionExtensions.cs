using Altibiz.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Ozds.Client.Components.Models;
using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.Options;

namespace Ozds.Client.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsClient(
    this IServiceCollection services
  )
  {
    services.AddOptions();
    services.AddModels();
    services.AddBlazor();
    services.AddLocalStorage();
    services.AddUi();
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureHubOptions>();
    services.ConfigureOptions<ConfigureCircuitOptions>();
    return services;
  }

  private static IServiceCollection AddModels(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IModelComponentProvider));
    services.AddScopedAssignableTo(typeof(IModelPageComponentProvider));
    services.AddScoped<ModelComponentProvider>();

    return services;
  }

  private static IServiceCollection AddBlazor(
    this IServiceCollection services
  )
  {
    services
      .AddRazorComponents()
      .AddInteractiveServerComponents();

    services.AddServerSideBlazor()
      .AddCircuitOptions(
        options => { options.DetailedErrors = true; });

    services.AddCascadingAuthenticationState();

    return services;
  }

  private static IServiceCollection AddUi(
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
}
