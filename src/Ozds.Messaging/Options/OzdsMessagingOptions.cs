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
}

public class ConfigureOzdsJobsOptions(
  IConfiguration configuration
) : IConfigureOptions<OzdsMessagingOptions>
{
  public void Configure(OzdsMessagingOptions options)
  {
    configuration.GetSection("Ozds:Messaging").Bind(options);
  }
}
