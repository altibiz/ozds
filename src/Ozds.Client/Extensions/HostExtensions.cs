using Altibiz.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Ozds.Client.Components.Models;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsClient(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddModels();
    builder.AddBlazor();
    builder.AddLocalStorage();
    builder.AddUi();
    return builder;
  }

  private static IHostApplicationBuilder AddModels(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IModelComponentProvider));
    builder.Services.AddScopedAssignableTo(typeof(IModelPageComponentProvider));
    builder.Services.AddScoped<ModelComponentProvider>();

    return builder;
  }

  private static IHostApplicationBuilder AddBlazor(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services
      .AddRazorComponents()
      .AddInteractiveServerComponents();

    builder.Services.AddServerSideBlazor()
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

    builder.Services.AddCascadingAuthenticationState();

    return builder;
  }

  private static IHostApplicationBuilder AddUi(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddMudServices();

    return builder;
  }

  private static IHostApplicationBuilder AddLocalStorage(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddBlazoredLocalStorage();

    return builder;
  }
}
