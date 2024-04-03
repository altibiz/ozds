using System.Reflection;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Iot;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries.Abstractions;
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

    services.AddScopedAssignableTo(typeof(IOzdsQueries));
    services.AddScopedAssignableTo(typeof(IOzdsMutations));

    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));

    services.AddScoped<OzdsIotHandler>();

    return services;
  }

  private static void AddScopedAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(type =>
        !type.IsAbstract &&
        !type.IsClass &&
        !type.IsGenericType &&
        type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddScoped(conversionType);
    }
  }

  private static void AddTransientAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(type =>
        !type.IsAbstract &&
        !type.IsClass &&
        !type.IsGenericType &&
        type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddTransient(conversionType);
    }
  }
}
