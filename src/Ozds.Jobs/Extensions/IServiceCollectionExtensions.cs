using Altibiz.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ozds.Jobs.Context;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Mutations.Abstractions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Options;
using Ozds.Jobs.Queries.Abstractions;
using Quartz;

namespace Ozds.Jobs.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsJobs(
    this IServiceCollection services
  )
  {
    services.AddOptions();
    services.AddObservers();
    services.AddManagers();
    services.AddQueries();
    services.AddMutations();
    services.AddJobs();
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsJobsOptions>();
    services.ConfigureOptions<ConfigureQuartzOptions>();
    services.ConfigureOptions<ConfigureQuartzHostedServiceOptions>();
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

  private static void AddJobs(
    this IServiceCollection services
  )
  {
    services.AddPooledDbContextFactory<JobsDbContext>(
      (services, options) =>
      {
        var jobsOptions = services
          .GetRequiredService<IOptions<OzdsJobsOptions>>().Value;

        options.UseNpgsql(
          jobsOptions.ConnectionString, x =>
          {
            x.MigrationsAssembly(
              typeof(JobsDbContext).Assembly.GetName().Name);
            x.MigrationsHistoryTable(
              $"__Ozds{nameof(JobsDbContext)}");
          });
      });

    services.AddQuartz();
  }
}
