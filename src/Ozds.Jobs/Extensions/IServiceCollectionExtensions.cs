using Microsoft.EntityFrameworkCore;
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
    services.Configure<OzdsJobsOptions>(
      builder.Configuration.GetSection("Ozds:Jobs"));

    var timersOptions = builder.Configuration
      .GetValue<OzdsJobsOptions>("Ozds:Jobs")
        ?? throw new InvalidOperationException(
          "Missing Ozds:Jobs configuration");

    services.AddQuartz(options =>
    {
      options.UsePersistentStore(options =>
      {
        options.UseSystemTextJsonSerializer();
        options.UsePostgres(options =>
        {
          options.ConnectionString = timersOptions.ConnectionString;
        });
      });
    });

    services.AddQuartzHostedService(options =>
    {
      options.WaitForJobsToComplete = true;
      options.AwaitApplicationStarted = true;
    });

    return services;
  }

  public static IApplicationBuilder UseOzdsJobs(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<OzdsJobsDbContext>();
    context.Database.Migrate();

    return app;
  }
}
