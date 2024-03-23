using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Data;
using Ozds.Data.Extensions;

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusinessClient(
    this IServiceCollection services
  )
  {
    services.AddDbContextFactory<OzdsDbContext>((services, options) => options
      .UseTimescale(
        services.GetRequiredService<IConfiguration>()
          .GetConnectionString("Ozds") ?? throw new InvalidOperationException(
          "Ozds connection string not found")
      )
      .AddServedSaveChangesInterceptorsFromAssembly(typeof(OzdsDbContext).Assembly, services)
    );
    services.AddScoped<OzdsRelationalQueries>();
    services.AddScoped<OzdsRelationalMutations>();

    return services;
  }
}
