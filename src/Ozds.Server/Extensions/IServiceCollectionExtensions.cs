using Altibiz.DependencyInjection.Extensions;

namespace Ozds.Server.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsServer(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.ConvertHostedServicesToModularTenantEvents();
    return services;
  }
}
