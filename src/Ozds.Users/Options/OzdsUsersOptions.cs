using Microsoft.Extensions.Options;

namespace Ozds.Users.Options;

public class OzdsUsersOptions
{
  public string ConnectionString { get; set; } = default!;

  public bool WithAuth { get; set; } = true;

  public bool MigrateOnStartup { get; set; } = false;
}

public class ConfigureOzdsUsersOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsUsersOptions>
{
  public void Configure(OzdsUsersOptions options)
  {
    configuration.GetSection("Ozds:Users").Bind(options);
  }

  public static bool WithAuth(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>("Ozds:Users:WithAuth") ?? true;
  }

  public static string ConnectionString(IConfiguration configuration)
  {
    return configuration.GetValue<string?>("Ozds:Users:ConnectionString")
      ?? string.Empty;
  }
}
