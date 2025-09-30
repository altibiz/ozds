using Microsoft.Extensions.Options;

namespace Ozds.Sdk.Options;

public class OzdsSdkOptions
{
  public string BaseUrl { get; set; } = default!;

  public string ApiKey { get; set; } = default!;
}

public class ConfigureOzdsSdkOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsSdkOptions>
{
  public void Configure(OzdsSdkOptions options)
  {
    configuration.GetSection("Ozds:Sdk").Bind(options);
  }

  public static string BaseUrl(IConfiguration configuration)
  {
    return configuration
        .GetValue<string>("Ozds:Sdk:BaseUrl")
      ?? throw new InvalidOperationException("OZDS SDK BaseUrl not set");
  }

  public static string ApiKey(IConfiguration configuration)
  {
    return configuration
        .GetValue<string>("Ozds:Sdk:ApiKey")
      ?? throw new InvalidOperationException("OZDS SDK ApiKey not set");
  }
}
