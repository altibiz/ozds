using MassTransit;
using Ozds.Fake.Options;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Entities;

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceStateSagaDefinition
  : SagaDefinition<NetworkUserInvoiceStateEntity>
{
  private const int ConcurrencyLimit = 20;

  public NetworkUserInvoiceStateSagaDefinition(IConfiguration configuration)
  {
    var options = configuration.GetSection(
        "Ozds:Fake:Messaging").Get<OzdsFakeMessagingOptions>()
      ?? throw new InvalidOperationException(
        "Ozds:Messaging not found in configuration");

    Endpoint(
      e =>
      {
        e.Name = options.Sagas.NetworkUserInvoiceState;
        e.PrefetchCount = ConcurrencyLimit;
      });
  }

  protected override void ConfigureSaga(
    IReceiveEndpointConfigurator endpointConfigurator,
    ISagaConfigurator<NetworkUserInvoiceStateEntity> sagaConfigurator,
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
