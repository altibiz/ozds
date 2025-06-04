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
using Ozds.Jobs.Services;
using Quartz;

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
    builder.AddServices();
    builder.AddJobs();
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
      });

    builder.Services.AddQuartz();
  }
}
