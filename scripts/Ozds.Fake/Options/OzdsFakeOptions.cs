using Microsoft.Extensions.Options;
using Ozds.Messaging.Options;

namespace Ozds.Fake.Options;

public class OzdsFakeOptions
{
  public OzdsFakeMessagingOptions Messaging { get; set; } = default!;
  public OzdsFakeClientOptions Client { get; set; } = default!;
}

public class OzdsFakeClientOptions
{
  public string BaseUrl { get; set; } = default!;
  public string ApiKey { get; set; } = default!;
}

public class OzdsFakeMessagingOptions
{
  public string ConnectionString { get; set; } = default!;
  public OzdsFakeMessagingEndpointsOptions Endpoints { get; set; } = default!;
  public OzdsFakeMessagingSagasOptions Sagas { get; set; } = default!;
}

public class OzdsFakeMessagingEndpointsOptions
{
  public string InitiateNetworkUserInvoice { get; set; } = default!;
  public string AbortNetworkUserInvoice { get; set; } = default!;
  public string RegisterNetworkUserInvoice { get; set; } = default!;
  public string ApproveNetworkUserInvoice { get; set; } = default!;
}

public class OzdsFakeMessagingSagasOptions
{
  public string NetworkUserInvoiceState { get; set; } = default!;
}

public class ConfigureOzdsFakeOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsFakeOptions>
{
  public void Configure(OzdsFakeOptions options)
  {
    configuration.GetSection("Ozds:Fake").Bind(options);
  }

  public static IOzdsMessagingParsedConnectionString ParseConnectionString(
    IConfiguration configuration
  )
  {
    var connectionString = configuration
        .GetValue<string?>("Ozds:Fake:Messaging:ConnectionString")
      ?? string.Empty;

    if (connectionString.StartsWith("amqp://"))
    {
      return new OzdsMessagingParsedRabbitMqConnectionString(connectionString);
    }

    throw new InvalidOperationException(
      "Only RabbitMQ is supported");
  }
}
