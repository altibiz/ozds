using Microsoft.Extensions.Options;

namespace Ozds.Time.Options;

#pragma warning disable S2094 // Classes should not be empty
public class OzdsTimeOptions
#pragma warning restore S2094 // Classes should not be empty
{
  public string RewindTimeStart { get; set; } = default!;
}

public class ConfigureOzdsTimeOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsTimeOptions>
{
  public void Configure(OzdsTimeOptions options)
  {
    configuration.GetSection("Ozds:Time").Bind(options);
  }
}
