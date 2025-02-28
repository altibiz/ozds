using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Options;

namespace Ozds.Messaging.Sagas;

public class NetworkUserInvoiceStateSagaDefinition
  : SagaDefinition<NetworkUserInvoiceStateEntity>
{
  private const int ConcurrencyLimit = 20;

  public NetworkUserInvoiceStateSagaDefinition(IConfiguration configuration)
  {
    var options = configuration.GetSection(
        "Ozds:Messaging").Get<OzdsMessagingOptions>()
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

#pragma warning disable S125
    // FIXME: nothing happens
    // endpointConfigurator.UseEntityFrameworkOutbox<OzdsMessagingDbContext>(context);
#pragma warning restore S125

    endpointConfigurator.UseInMemoryOutbox(context);

    var partition = endpointConfigurator.CreatePartitioner(ConcurrencyLimit);

    sagaConfigurator.Message<IAbortNetworkUserInvoice>(
      x => x
        .UsePartitioner(partition, m => m.Message.NetworkUserInvoiceId));
    sagaConfigurator.Message<IInitiateNetworkUserInvoice>(
      x => x
        .UsePartitioner(partition, m => m.Message.NetworkUserInvoiceId));
    sagaConfigurator.Message<IRegisterNetworkUserInvoice>(
      x => x
        .UsePartitioner(partition, m => m.Message.NetworkUserInvoiceId));
  }
}
