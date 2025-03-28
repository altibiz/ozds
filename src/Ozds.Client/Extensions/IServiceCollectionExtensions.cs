using Altibiz.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Ozds.Client.Components.Models;
using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.Conversion;
using Ozds.Client.Conversion.Abstractions;
using Ozds.Client.Export.Abstractions;
using Ozds.Client.Import.Abstractions;
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
    services.AddConversion();
    services.AddImport();
    services.AddExport();
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

    services.AddServerSideBlazor();

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

  public static IServiceCollection AddConversion(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IModelRecordConverter));
    services.AddSingleton(typeof(ModelRecordConverter));
    return services;
  }

  public static IServiceCollection AddImport(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IImporter));
    return services;
  }

  public static IServiceCollection AddExport(
    this IServiceCollection services
  )
  {
    services.AddTransientAssignableTo(typeof(IExporter));
    return services;
  }
}
