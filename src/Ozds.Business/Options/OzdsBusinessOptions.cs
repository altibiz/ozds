using Microsoft.Extensions.Options;

namespace Ozds.Business.Options;

public class OzdsBusinessOptions
{
  public bool WithReactors { get; set; } = true;

  public OzdsBusinessReactorOptions Reactor { get; set; } = new();
}

public class OzdsBusinessReactorOptions
{
  public double MeasurementDeletionJobIntervalSeconds { get; set; } =
    TimeSpan.FromDays(90).TotalSeconds;
}

public class ConfigureOzdsBusinessOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsBusinessOptions>
{
  public void Configure(OzdsBusinessOptions options)
  {
    configuration.GetSection("Ozds:Business").Bind(options);
  }

  public static bool WithReactors(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>("Ozds:Business:WithReactors")
      ?? true;
  }
}
