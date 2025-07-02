using Microsoft.Extensions.Options;

namespace Ozds.Messaging.Options;

public class OzdsMessagingEndpointOptions
{
  public string AcknowledgeNetworkUserInvoice { get; set; } = default!;
}

public class OzdsMessagingSagaOptions
{
  public string NetworkUserInvoiceState { get; set; } = default!;
}

public class OzdsMessagingOptions
{
  public string ConnectionString { get; set; } = default!;

  public string PersistenceConnectionString { get; set; } = default!;

  public OzdsMessagingEndpointOptions Endpoints { get; set; } = default!;

  public OzdsMessagingSagaOptions Sagas { get; set; } = default!;

  public bool WithBus { get; set; } = true;

  public bool MigrateOnStartup { get; set; } = false;
}

public interface IOzdsMessagingParsedConnectionString
{
}

public class OzdsMessagingParsedRabbitMqConnectionString
  : IOzdsMessagingParsedConnectionString
{
  public OzdsMessagingParsedRabbitMqConnectionString(
    string connectionString
  )
  {
    var dictionary = connectionString
      .Replace("amqp://", "")
      .Split(';')
      .ToDictionary(
        x => x.Split('=')[0],
        x => string.Join('=', x.Split('=')[1..]));

    Host = dictionary["Host"];
    VirtualHost = dictionary["VirtualHost"];
    Port = int.Parse(dictionary["Port"]);
    User = dictionary["Username"];
    Password = dictionary["Password"];
  }

  public string Host { get; init; }

  public string VirtualHost { get; init; }

  public int Port { get; init; }

  public string User { get; init; }

  public string Password { get; init; }
}

public class OzdsMessagingParsedAzureServiceBusConnectionString
  : IOzdsMessagingParsedConnectionString
{
  public OzdsMessagingParsedAzureServiceBusConnectionString(
    string connectionString
  )
  {
    ConnectionString = connectionString;
  }

  public string ConnectionString { get; set; }
}

public class ConfigureOzdsMessagingOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsMessagingOptions>
{
  public void Configure(OzdsMessagingOptions options)
  {
    configuration.GetSection("Ozds:Messaging").Bind(options);
  }

  public static bool WithBus(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>("Ozds:Messaging:WithBus") ?? true;
  }

  public static IOzdsMessagingParsedConnectionString ParseConnectionString(
    IConfiguration configuration
  )
  {
    var connectionString = configuration
        .GetValue<string?>("Ozds:Messaging:ConnectionString")
      ?? string.Empty;

    if (connectionString.StartsWith("amqp://"))
    {
      return new OzdsMessagingParsedRabbitMqConnectionString(connectionString);
    }

    return new OzdsMessagingParsedAzureServiceBusConnectionString(
      connectionString);
  }
}
