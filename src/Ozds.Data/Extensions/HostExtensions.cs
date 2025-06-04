using System.Reflection;
using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Options;
using Ozds.Data.Procedures.Abstractions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Services;

namespace Ozds.Data.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsData(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddQueries();
    builder.AddMutations();
    builder.AddProcedures();
    builder.AddServices();
    builder.AddObservers();
    builder.AddDatabase();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<OzdsDataConfigureOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddObservers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IPublisher));
    builder.Services.AddSingletonAssignableTo(typeof(ISubscriber));
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IQueries));
    return builder;
  }

  private static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMutations));
    return builder;
  }

  private static IHostApplicationBuilder AddProcedures(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IProcedures));
    return builder;
  }

  private static IHostApplicationBuilder AddServices(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddHostedService<MigrationService>();
    return builder;
  }

  private static void AddDatabase(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddPooledDbContextFactory<DataDbContext>(
      (services, options) =>
      {
        var dataOptions = services
          .GetRequiredService<IOptions<OzdsDataOptions>>().Value;
        var environment = services
          .GetRequiredService<IHostEnvironment>();

        if (environment.IsDevelopment() && dataOptions.LogSql)
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
          .AddServedSaveChangesInterceptorsFromAssembly(
            typeof(HostExtensions).Assembly,
            services
          );

        if (environment.IsDevelopment())
        {
          options.ConfigureWarnings(
            warnings => warnings
              .Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }

        if (dataOptions.UseProxies)
        {
          options = options.UseLazyLoadingProxies();
        }

        options.UseSnakeCaseNamingConvention();
      });
  }
}
