using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Ozds.Jobs.Context;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Options;
using Quartz;

namespace Ozds.Jobs.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsJobs(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddOptions(builder);
    services.AddObservers();
    services.AddManagers();
    services.AddQuartz(builder);
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsJobsOptions>(
      builder.Configuration.GetSection("Ozds:Jobs"));
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

  private static IServiceCollection AddManagers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IJobManager));
    return services;
  }

  private static void AddQuartz(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    var jobsOptions = builder.Configuration
        .GetSection("Ozds:Jobs")
        .Get<OzdsJobsOptions>()
      ?? throw new InvalidOperationException(
        "Missing Ozds:Jobs configuration");

    services.AddQuartz(
      options =>
      {
        options.InterruptJobsOnShutdown = true;
        options.UsePersistentStore(
          options =>
          {
            options.UseSystemTextJsonSerializer();
            options.UsePostgres(
              options =>
              {
                options.ConnectionString = jobsOptions.ConnectionString;
              });
          });
      });

    services.AddPooledDbContextFactory<JobsDbContext>(
      options =>
      {
        options.UseNpgsql(
          jobsOptions.ConnectionString, x =>
          {
            x.MigrationsAssembly(
              typeof(JobsDbContext).Assembly.GetName().Name);
            x.MigrationsHistoryTable(
              $"__Ozds{nameof(JobsDbContext)}");
          });
      });

    services.Configure<QuartzHostedServiceOptions>(
      options => { options.AwaitApplicationStarted = true; });
    services.AddSingleton<IHostedService, QuartzHostedService>(
      services =>
      {
        using var context = services
          .GetRequiredService<IDbContextFactory<JobsDbContext>>()
          .CreateDbContext();
        context.Database.Migrate();
        return ActivatorUtilities
          .CreateInstance<QuartzHostedService>(services);
      }
    );
  }
}
