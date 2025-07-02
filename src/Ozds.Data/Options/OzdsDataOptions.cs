using Microsoft.Extensions.Options;

namespace Ozds.Data.Options;

public class OzdsDataOptions
{
  public string ConnectionString { get; set; } = default!;

  public bool UseProxies { get; set; } = true;

  public bool LogSql { get; set; } = false;

  public bool MigrateOnStartup { get; set; } = false;
}

public class OzdsDataConfigureOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsDataOptions>
{
  public void Configure(OzdsDataOptions options)
  {
    configuration.GetSection("Ozds:Data").Bind(options);
  }
}
