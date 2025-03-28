using Microsoft.Extensions.Options;
using Ozds.Messaging.Options;

namespace Ozds.Migration.Options;

public class ConfigureOzdsMessagingOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsMessagingOptions>
{
  public void Configure(OzdsMessagingOptions options)
  {
    configuration.GetSection("Ozds:Migration:Messaging").Bind(options);
  }
}
