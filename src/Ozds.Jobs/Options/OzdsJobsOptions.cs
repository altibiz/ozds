using Microsoft.Extensions.Options;

namespace Ozds.Jobs.Options;

public class OzdsJobsOptions
{
  public string ConnectionString { get; set; } = default!;
}

public class ConfigureOzdsJobsOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsJobsOptions>
{
  public void Configure(OzdsJobsOptions options)
  {
    configuration.GetSection("Ozds:Jobs").Bind(options);
  }
}
