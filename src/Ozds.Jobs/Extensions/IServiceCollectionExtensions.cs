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
    // Options
    services.Configure<OzdsJobsOptions>(
      builder.Configuration.GetSection("Ozds:Jobs"));

    // Quartz
    services.AddQuartz(builder);

    // Managers
    services.AddSingletonAssignableTo(typeof(IJobManager));

    // Observers
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));

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

    services.AddQuartzHostedService(
      options =>
      {
        options.WaitForJobsToComplete = true;
        options.AwaitApplicationStarted = true;
      });

    services.AddDbContextFactory<JobsDbContext>(
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

    // NOTE: this needs jesus
    services.AddSingleton<IHostedService, QuartzHostedService>(
      services =>
      {
        using var context = services
          .GetRequiredService<IDbContextFactory<JobsDbContext>>()
          .CreateDbContext();
        context.Database.Migrate();

        return ActivatorUtilities.CreateInstance<QuartzHostedService>(services);
      }
    );
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
          type.IsClass &&
          type.IsAssignableTo(assignableTo));

    foreach (var conversionType in conversionTypes)
    {
      services.AddSingleton(conversionType);
      foreach (var interfaceType in conversionType.GetAllInterfaces())
      {
        services.AddSingleton(
          interfaceType, services =>
            services.GetRequiredService(conversionType));
      }
    }
  }

  private static Type[] GetAllInterfaces(this Type type)
  {
    return type.GetInterfaces()
      .Concat(type.GetInterfaces().SelectMany(GetAllInterfaces))
      .Concat(type.BaseType?.GetAllInterfaces() ?? Array.Empty<Type>())
      .Distinct()
      .ToArray();
  }
}
