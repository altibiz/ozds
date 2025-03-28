using System.Reflection;
using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Options;
using Ozds.Data.Procedures.Abstractions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsData(
    this IServiceCollection services
  )
  {
    services.AddOptions();
    services.AddProcedures();
    services.AddQueries();
    services.AddMutations();
    services.AddDatabase();
    services.AddObservers();
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<OzdsDataConfigureOptions>();
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

  private static IServiceCollection AddProcedures(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IProcedures));
    return services;
  }

  private static void AddDatabase(
    this IServiceCollection services
  )
  {
    services.AddPooledDbContextFactory<DataDbContext>(
      (services, options) =>
      {
        var dataOptions = services
          .GetRequiredService<IOptions<OzdsDataOptions>>().Value;
        var environment = services
          .GetRequiredService<IHostEnvironment>();

        if (environment.IsDevelopment()
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
          .UseNpgsql(
            dataSource,
            options =>
            {
              options.MigrationsAssembly(
                typeof(DataDbContext).Assembly.GetName().Name);
              options.MigrationsHistoryTable(
                $"__Ozds{nameof(DataDbContext)}");
            })
          .UseTimescale()
          .UseServedMigrationsAssembly()
          .AddServedSaveChangesInterceptorsFromAssembly(
            typeof(IServiceCollectionExtensions).Assembly,
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
