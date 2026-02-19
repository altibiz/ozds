using MassTransit;
using Microsoft.Extensions.Options;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Options;
using Ozds.Messaging.Sender.Abstractions;

// NOTE: no hard dependency on MassTransit because we might not use
// it with the bus

namespace Ozds.Messaging.Sender.Implementations;

public class EndpointMessageSender(
  IServiceProvider serviceProvider,
  IOptions<OzdsMessagingOptions> options
) : IMessageSender
{
  public async Task AcknowledgeNetworkUserInvoice(
    IAcknowledgeNetworkUserInvoice acknowledgeNetworkUserInvoice,
    CancellationToken cancellationToken
  )
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    var endpointProvider =
      scope.ServiceProvider.GetService<ISendEndpointProvider>();
    if (endpointProvider is null)
    {
      throw new InvalidOperationException("No send endpoint provider found");
    }

    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(options.Value.Endpoints.AcknowledgeNetworkUserInvoice)
    );

    await endpoint.Send(acknowledgeNetworkUserInvoice, cancellationToken);
  }

  public async Task AcknowledgeNetworkUserInvoices(
    IEnumerable<IAcknowledgeNetworkUserInvoice> acknowledgeNetworkUserInvoices,
    CancellationToken cancellationToken
  )
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    var endpointProvider =
      scope.ServiceProvider.GetService<ISendEndpointProvider>();
    if (endpointProvider is null)
    {
      throw new InvalidOperationException("No send endpoint provider found");
    }

    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(options.Value.Endpoints.AcknowledgeNetworkUserInvoice)
    );

    await endpoint.SendBatch(acknowledgeNetworkUserInvoices, cancellationToken);
  }
}
