using System.Reflection;
using Ozds.Business.Iot;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Base;
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
      .AddServedSaveChangesInterceptorsFromAssembly(
        Assembly.GetExecutingAssembly(),
        services
      )
    );

    services.AddScoped<OzdsAuditableMutations>();
    services.AddScoped<OzdsEventMutations>();
    services.AddScoped<OzdsInvoiceMutations>();
    services.AddScoped<OzdsMeasurementMutations>();

    services.AddScoped<OzdsAuditableQueries>();
    services.AddScoped<OzdsEventQueries>();
    services.AddScoped<OzdsMeasurementQueries>();
    services.AddScoped<OzdsAggregateQueries>();
    services.AddScoped<OzdsInvoiceQueries>();

    services.AddScoped<OzdsRepresentativeQueries>();
    services.AddScoped<OzdsNetworkUserQueries>();
    services.AddScoped<OzdsBlueLowCatalogueModelQueries>();
    services.AddScoped<OzdsRedLowCatalogueModelQueries>();
    services.AddScoped<OzdsWhiteLowCatalogueModelQueries>();
    services.AddScoped<OzdsWhiteMediumCatalogueModelQueries>();

    services.AddScoped<OzdsIotHandler>();

    return services;
  }
}
