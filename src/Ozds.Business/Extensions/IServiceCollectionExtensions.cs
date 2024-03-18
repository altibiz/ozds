using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Data;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusinessClient(
    this IServiceCollection services)
  {
    services.AddScoped<OzdsDbContext>();
    services.AddScoped<OzdsRelationalQueries>();
    services.AddScoped<OzdsRelationalMutations>();

    return services;
  }
}
