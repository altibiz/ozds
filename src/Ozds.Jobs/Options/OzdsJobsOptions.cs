using Microsoft.Extensions.Options;

namespace Ozds.Jobs.Options;

public class OzdsJobsOptions
{
  public string ConnectionString { get; set; } = default!;

  public bool MigrateOnStartup { get; set; } = false;

  public bool WithServices { get; set; } = true;

  public OzdsJobsArchivalOptions Archival { get; set; } = new();

  public OzdsJobsBillingOptions Billing { get; set; } = new();
}

public class OzdsJobsArchivalOptions
{
  public string DailyMeasurementDeletionCron { get; set; } =
    "0 0 0 * * ?"; // NOTE: on the first second of every day
}

public class OzdsJobsBillingOptions
{
  public string MonthlyBillingCron { get; set; } =
    "0 0 0 1 * ?"; // NOTE: on the first second of every month
}

public class ConfigureOzdsJobsOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsJobsOptions>
{
  public void Configure(OzdsJobsOptions options)
  {
    configuration.GetSection("Ozds:Jobs").Bind(options);
  }

  public static bool WithServices(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>("Ozds:Jobs:WithServices") ?? true;
  }
}
