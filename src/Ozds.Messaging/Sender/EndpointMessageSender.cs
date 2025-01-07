using MassTransit;
using Microsoft.Extensions.Options;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Options;
using Ozds.Messaging.Sender.Abstractions;

namespace Ozds.Messaging.Sender;

public class EndpointMessageSender(
  ISendEndpointProvider endpointProvider,
  IOptions<OzdsMessagingOptions> options
) : IMessageSender
{
  public async Task AcknowledgeNetworkUserInvoice(
    IAcknowledgeNetworkUserInvoice acknowledgeNetworkUserInvoice,
    CancellationToken cancellationToken
  )
  {
    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(options.Value.Endpoints.AcknowledgeNetworkUserInvoice));

    await endpoint.Send(
      acknowledgeNetworkUserInvoice,
      cancellationToken
    );
  }
}
