using Microsoft.EntityFrameworkCore;
using Ozds.Timers.Options;
using Quartz;

namespace Ozds.Timers.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsTimers(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsTimersOptions>(
      builder.Configuration.GetSection("Ozds:Timers"));

    var timersOptions = builder.Configuration
      .GetValue<OzdsTimersOptions>("Ozds:Timers")
        ?? throw new InvalidOperationException(
          "Missing Ozds:Timers configuration");

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

  public static IApplicationBuilder UseOzdsTimers(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    using var scope = app.ApplicationServices.CreateScope();
    var context = scope.ServiceProvider
      .GetRequiredService<OzdsTimersDbContext>();
    context.Database.Migrate();

    return app;
  }
}
