using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Context;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Mutations.Abstractions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Options;
using Ozds.Jobs.Queries.Abstractions;
using Ozds.Jobs.Scheduler;
using Ozds.Jobs.Services;
using Quartz;
using Quartz.Logging;
using LogLevel = Quartz.Logging.LogLevel;

namespace Ozds.Jobs.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsJobs(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddObservers();
    builder.AddManagers();
    builder.AddQueries();
    builder.AddMutations();
    builder.AddJobs();

    if (ConfigureOzdsJobsOptions.WithServices(builder.Configuration))
    {
      builder.AddServices();
    }

    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsJobsOptions>();
    builder.Services.ConfigureOptions<ConfigureQuartzOptions>();
    builder.Services.ConfigureOptions<ConfigureQuartzHostedServiceOptions>();
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

  private static IHostApplicationBuilder AddManagers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingleton<OzdsSchedulerFactory>();
    builder.Services.AddHostedService(
      x => x
        .GetRequiredService<OzdsSchedulerFactory>());
    builder.Services.AddSingletonAssignableTo(typeof(IJobManager));
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

  private static IHostApplicationBuilder AddServices(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddHostedService<MigrationService>();
    return builder;
  }

  private static void AddJobs(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddQuartz();

    builder.Services.AddPooledDbContextFactory<JobsDbContext>(
      (services, options) =>
      {
        var jobsOptions = services
          .GetRequiredService<IOptions<OzdsJobsOptions>>().Value;
        var environment = services
          .GetRequiredService<IHostEnvironment>();

        options.UseNpgsql(
          jobsOptions.ConnectionString, x =>
          {
            x.MigrationsAssembly(
              typeof(JobsDbContext).Assembly.GetName().Name);
            x.MigrationsHistoryTable(
              $"__Ozds{nameof(JobsDbContext)}");
          });

        if (environment.IsDevelopment())
        {
          options.ConfigureWarnings(
            warnings => warnings
              .Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        }

        // FIXME: log provider is here because we can get to
        // the IServiceProvider from here
        LogProvider.SetCurrentLogProvider(
          new QuartzAspNetCoreLogProvider(
            services.GetRequiredService<ILoggerFactory>()));
      });
  }

  private sealed class QuartzAspNetCoreLogProvider(
    ILoggerFactory loggerFactory
  ) : ILogProvider
  {
    public Logger GetLogger(string name)
    {
      ILogger logger;
      try
      {
        logger = loggerFactory.CreateLogger(name);
      }
      catch (ObjectDisposedException)
      {
        return (_, _, _, _) => { return false; };
      }

      return (level, func, exception, parameters) =>
      {
        logger.Log(
          level switch
          {
            LogLevel.Fatal =>
              Microsoft.Extensions.Logging.LogLevel.Critical,
            LogLevel.Error =>
              Microsoft.Extensions.Logging.LogLevel.Error,
            LogLevel.Warn =>
              Microsoft.Extensions.Logging.LogLevel.Warning,
            LogLevel.Info =>
              Microsoft.Extensions.Logging.LogLevel.Information,
            LogLevel.Debug =>
              Microsoft.Extensions.Logging.LogLevel.Debug,
            LogLevel.Trace =>
              Microsoft.Extensions.Logging.LogLevel.Trace,
            _ => Microsoft.Extensions.Logging.LogLevel.Information
          },
          exception,
#pragma warning disable CA2254 // Template should be a static expression
          func is { } f ? f() : null,
#pragma warning restore CA2254 // Template should be a static expression
          parameters);

        return true;
      };
    }

    public IDisposable OpenNestedContext(string message)
    {
      throw new NotImplementedException();
    }

    public IDisposable OpenMappedContext(
      string key,
      object value,
      bool destructure = false)
    {
      throw new NotImplementedException();
    }
  }
}
