using Microsoft.Extensions.Options;

namespace Ozds.Migration.Options;

#pragma warning disable S2094 // Classes should not be empty
public class OzdsMigrationOptions
#pragma warning restore S2094 // Classes should not be empty
{ }

public class ConfigureOzdsMigrationOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsMigrationOptions>
{
  public void Configure(OzdsMigrationOptions options)
  {
    configuration.GetSection("Ozds:Migration").Bind(options);
  }
}
