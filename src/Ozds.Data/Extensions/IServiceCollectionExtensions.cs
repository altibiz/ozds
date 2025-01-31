using System.Reflection;
using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Options;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsData(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddOptions(builder);
    services.AddObservers();
    services.AddQueries();
    services.AddMutations();
    services.AddEntityFrameworkCore(builder);
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsDataOptions>(
      builder.Configuration.GetSection("Ozds:Data"));
    return services;
  }

  private static IServiceCollection AddObservers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));
    return services;
  }

  private static IServiceCollection AddQueries(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IQueries));
    return services;
  }

  private static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IMutations));
    return services;
  }

  private static void AddEntityFrameworkCore(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var dataOptions =
      builder.Configuration
        .GetSection("Ozds:Data")
        .Get<OzdsDataOptions>()
      ?? throw new InvalidOperationException(
        "Ozds:Data not found in configuration"
      );

    services.AddPooledDbContextFactory<DataDbContext>(
      (services, options) =>
      {
        if (builder.Environment.IsDevelopment()
          && Environment.GetEnvironmentVariable("OZDS_LOG_SQL") is not null)
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

        if (dataOptions.UseProxies)
        {
          options = options.UseLazyLoadingProxies();
        }

        options.UseSnakeCaseNamingConvention();
      });
  }
}
