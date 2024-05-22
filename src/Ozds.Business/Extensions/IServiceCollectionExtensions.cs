using System.Reflection;
using System.Text.Json;
using OrchardCore.Environment.Shell;
using Ozds.Business.Aggregation;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Finance;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Iot;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Extensions;

// TODO: switch to enable query/mutation logging
// TODO: remove references to OrchardCore

namespace Ozds.Business.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsBusinessClient(
    this IServiceCollection services,
    bool IsDevelopment
  )
  {
    services.AddDbContextFactory<OzdsDbContext>((services, options) =>
    {
      if (IsDevelopment)
      {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        // options.UseLoggerFactory(
        //   LoggerFactory.Create(builder => builder.AddConsole())
        // );
      }

      var shellConfiguration =
        services.GetRequiredService<ShellSettings>().ShellConfiguration;
      var connectionString = shellConfiguration["ConnectionString"]
                             ?? shellConfiguration
                               .GetSection("OrchardCore_AutoSetup")
                               ?.GetSection("Tenants")
                               ?.GetSection("0")
                               ?["DatabaseConnectionString"];
      if (connectionString is null)
      {
        var serializerOptions = new JsonSerializerOptions
        {
          WriteIndented = true
        };
        var serializedShellConfiguration = JsonSerializer.Serialize(
          shellConfiguration.AsEnumerable().ToList(),
          serializerOptions
        );
        throw new InvalidOperationException(
          "ConnectionString not found in shell configuration: "
          + serializedShellConfiguration
        );
      }

      options
        .UseTimescale(connectionString)
        .AddServedSaveChangesInterceptorsFromAssembly(
          Assembly.GetExecutingAssembly(),
          services
        );
    });

    services.AddScopedAssignableTo(typeof(IOzdsQueries));
    services.AddScopedAssignableTo(typeof(IOzdsMutations));

    services.AddTransientAssignableTo(typeof(IAggregateUpserter));
    services.AddSingleton(typeof(AgnosticAggregateUpserter));

    services.AddTransientAssignableTo(typeof(IModelEntityConverter));
    services.AddSingleton(typeof(AgnosticModelEntityConverter));
    services.AddTransientAssignableTo(typeof(IMeasurementAggregateConverter));
    services.AddSingleton(typeof(AgnosticMeasurementAggregateConverter));
    services.AddTransientAssignableTo(typeof(IPushRequestMeasurementConverter));
    services.AddSingleton(typeof(AgnosticPushRequestMeasurementConverter));

    services.AddTransient(typeof(NetworkUserInvoiceCalculator));
    services.AddTransientAssignableTo(
      typeof(INetworkUserCalculationCalculator));
    services.AddSingleton(typeof(AgnosticNetworkUserCalculationCalculator));
    services.AddTransientAssignableTo(typeof(ICalculationItemCalculator));
    services.AddSingleton(typeof(AgnosticCalculationItemCalculator));

    services.AddScoped<OzdsIotHandler>();
    services.AddScoped<BatchAggregatedMeasurementUpserter>();

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
        !type.IsGenericType &&
        type.IsClass &&
        type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddScoped(assignableTo, conversionType);
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
        !type.IsGenericType &&
        type.IsClass &&
        type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddTransient(assignableTo, conversionType);
      services.AddTransient(conversionType);
    }
  }
}
