using System.Reflection;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Options;

namespace Ozds.Data.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsData(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    // Entity Framework Core
    services.AddEntityFrameworkCore(builder);

    // Observers
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));

    return services;
  }

  private static void AddSingletonAssignableTo(
    this IServiceCollection services,
    Type assignableTo
  )
  {
    var conversionTypes = typeof(IServiceCollectionExtensions).Assembly
      .GetTypes()
      .Where(
        type =>
          !type.IsAbstract &&
          !type.IsGenericType &&
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddSingleton(conversionType);
        services.AddSingleton(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static void AddEntityFrameworkCore(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsDataOptions>(
      builder.Configuration.GetSection("Ozds:Data"));

    var dataOptions =
      builder.Configuration
        .GetSection("Ozds:Data")
        .Get<OzdsDataOptions>()
      ?? throw new InvalidOperationException(
        "Ozds:Data not found in configuration"
      );

    services.AddDbContextFactory<DataDbContext>(
      (services, options) =>
      {
        if (builder.Environment.IsDevelopment()
          && Environment.GetEnvironmentVariable("ENMS_LOG_SQL") is not null)
        {
          options.EnableSensitiveDataLogging();
          options.EnableDetailedErrors();
          options.UseLoggerFactory(
            LoggerFactory.Create(builder => builder.AddConsole())
          );
        }

        var dataSourceBuilder =
          new NpgsqlDataSourceBuilder(dataOptions.ConnectionString);
        dataSourceBuilder.ApplyConfigurationsFromAssembly(
          Assembly.GetExecutingAssembly());
        var dataSource = dataSourceBuilder.Build();

        options
          .UseTimescale(
            dataSource,
            options =>
            {
              options.MigrationsAssembly(
                typeof(DataDbContext).Assembly.GetName().Name);
              options.MigrationsHistoryTable(
                $"__Ozds{nameof(DataDbContext)}");
            })
          .AddServedSaveChangesInterceptorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            services
          );
      });
  }

  private static Type[] GetAllInterfaces(this Type type)
  {
    return type.GetInterfaces()
      .Concat(type.GetInterfaces().SelectMany(GetAllInterfaces))
      .Concat(type.BaseType?.GetAllInterfaces() ?? Array.Empty<Type>())
      .ToHashSet()
      .ToArray();
  }
}
