using Microsoft.EntityFrameworkCore;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Data;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusinessClient(
    this IServiceCollection services,
    string connectionString
  )
  {
    services.AddDbContext<OzdsDbContext>(
      options => options.UseNpgsql(connectionString)
    );
    services.AddScoped<OzdsRelationalQueries>();
    services.AddScoped<OzdsRelationalMutations>();

    return services;
  }
}
