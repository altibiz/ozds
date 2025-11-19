using Microsoft.Extensions.Options;

namespace Ozds.Caching.Options;

public class OzdsCachingOptions
{
  public string ConnectionString { get; set; } = default!;
}

public class ConfigureOzdsCachingOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsCachingOptions>
{
  public void Configure(OzdsCachingOptions options)
  {
    configuration.GetSection("Ozds:Caching").Bind(options);
  }

  public static string ConnectionString(IConfiguration configuration)
  {
    return configuration.GetValue<string>("Ozds:Caching:ConnectionString")
      ?? throw new InvalidOperationException("Connection string not found");
  }
}
