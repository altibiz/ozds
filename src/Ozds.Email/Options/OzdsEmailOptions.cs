using Microsoft.Extensions.Options;

namespace Ozds.Email.Options;

public class OzdsEmailFromOptions
{
  public string Name { get; set; } = string.Empty;

  public string Address { get; set; } = string.Empty;
}

public class OzdsEmailSmtpOptions
{
  public string ConnectionString { get; set; } = string.Empty;
}

public class OzdsEmailOptions
{
  public OzdsEmailSmtpOptions Smtp { get; set; } = new();

  public OzdsEmailFromOptions From { get; set; } = new();
}

public class OzdsEmailParsedSmtpConnectionString
{
  public OzdsEmailParsedSmtpConnectionString(
    string connectionString
  )
  {
    var dictionary = connectionString
      .Split(';')
      .ToDictionary(
        x => x.Split('=')[0],
        x => string.Join('=', x.Split('=')[1..]));

    Host = dictionary["Host"];
    Port = int.Parse(dictionary["Port"]);
    User = dictionary["User"];
    Password = dictionary["Password"];
    Ssl = bool.Parse(dictionary["Ssl"]);
  }

  public string Host { get; set; }

  public int Port { get; set; }

  public string User { get; set; }

  public string Password { get; set; }

  public bool Ssl { get; set; }
}

public class ConfigureOzdsEmailOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsEmailOptions>
{
  public void Configure(OzdsEmailOptions options)
  {
    configuration.GetSection("Ozds:Email").Bind(options);
  }
}
