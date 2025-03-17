using Microsoft.Extensions.Options;
using Ozds.Jobs.Options;

namespace Ozds.Migration.Options;

public class ConfigureOzdsJobsOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsJobsOptions>
{
  public void Configure(OzdsJobsOptions options)
  {
    configuration.GetSection("Ozds:Migration:Jobs").Bind(options);
  }
}
