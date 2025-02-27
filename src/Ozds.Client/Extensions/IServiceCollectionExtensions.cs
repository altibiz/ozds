using Altibiz.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Ozds.Client.Components.Models;
using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.Conversion;
using Ozds.Client.Conversion.Abstractions;
using Ozds.Client.Import.Abstractions;

namespace Ozds.Client.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsClient(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddModels();
    services.AddBlazor(builder);
    services.AddLocalStorage();
    services.AddUi();
    services.AddConversion();
    services.AddImport();
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
}
