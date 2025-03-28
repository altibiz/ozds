using Microsoft.Extensions.Options;
using Ozds.Data.Options;

namespace Ozds.Migration.Options;

public class ConfigureOzdsDataOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsDataOptions>
{
  public void Configure(OzdsDataOptions options)
  {
    configuration.GetSection("Ozds:Migration:Data").Bind(options);
  }
}
