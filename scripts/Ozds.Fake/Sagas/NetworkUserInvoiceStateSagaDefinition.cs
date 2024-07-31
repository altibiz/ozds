using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceStateSagaDefinition
  : SagaDefinition<NetworkUserInvoiceState>
{
  private const int ConcurrencyLimit = 20;

  public NetworkUserInvoiceStateSagaDefinition(IConfiguration configuration)
  {
    var config = configuration
      .GetSection("Ozds")
      .GetSection("Messaging")
      .GetSection("Sagas");

    Endpoint(
      e =>
      {
        e.Name = config["NetworkUserInvoiceState"]
          ?? throw new InvalidOperationException(
            "NetworkUserInvoiceState endpoint not found");
        e.PrefetchCount = ConcurrencyLimit;
      });
  }

  protected override void ConfigureSaga(
    IReceiveEndpointConfigurator endpointConfigurator,
    ISagaConfigurator<NetworkUserInvoiceState> sagaConfigurator,
    IRegistrationContext context
  )
  {
    endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
    endpointConfigurator.UseInMemoryOutbox(context);

    var partition = endpointConfigurator.CreatePartitioner(ConcurrencyLimit);

    sagaConfigurator.Message<IAcknowledgeNetworkUserInvoice>(
      x => x
        .UsePartitioner(partition, m => m.Message.NetworkUserInvoiceId));
  }
}
