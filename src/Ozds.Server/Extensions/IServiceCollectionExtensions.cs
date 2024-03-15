using MudBlazor.Services;

namespace Ozds.Server.Extensions;

public static class IServiceCollectionExtensions
{
  public static void AddOzdsClient(this IServiceCollection services, bool isDevelopment)
  {
    services
      .AddRazorComponents()
      .AddInteractiveServerComponents();
    services
      .AddServerSideBlazor()
      .AddCircuitOptions(options =>
      {
        if (isDevelopment)
        {
          options.DetailedErrors = true;
        }
      })
      .AddHubOptions(options =>
      {
        if (isDevelopment)
        {
          options.EnableDetailedErrors = true;
        }
      });
    services.AddMudServices();
    services.AddCascadingAuthenticationState();
  }
}
