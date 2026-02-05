using Microsoft.Extensions.Options;
using Quartz;

namespace Ozds.Jobs.Options;

public class ConfigureQuartzOptions(IOptions<OzdsJobsOptions> jobsOptions)
  : IConfigureOptions<QuartzOptions>
{
  public void Configure(QuartzOptions options)
  {
    var builder = SchedulerBuilder.Create();
    builder.InterruptJobsOnShutdown = true;
    builder.UsePersistentStore(builder =>
    {
      builder.UseSystemTextJsonSerializer();
      builder.UsePostgres(builder =>
      {
        builder.ConnectionString = jobsOptions.Value.ConnectionString;
      });
    });

    foreach (var key in builder.Properties.AllKeys)
    {
      if (key is null)
      {
        continue;
      }

      options[key] = builder.Properties[key];
    }
  }
}
