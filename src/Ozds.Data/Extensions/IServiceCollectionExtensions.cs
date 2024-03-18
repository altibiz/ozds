namespace Ozds.Data.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsDataClient(
    this IServiceCollection services)
  {
    services.AddScoped<OzdsDbContext>();
    services.AddScoped<OzdsDbClient>();

    return services;
  }
}
