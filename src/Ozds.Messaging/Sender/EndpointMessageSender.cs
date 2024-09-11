using MassTransit;
using Microsoft.Extensions.Options;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Options;
using Ozds.Messaging.Sender.Abstractions;

namespace Ozds.Messaging.Sender;

public class EndpointMessageSender(
  ISendEndpointProvider endpointProvider,
  IOptions<OzdsMessagingEndpointOptions> options
) : IMessageSender
{
  public async Task AcknowledgeNetworkUserInvoice(
    IAcknowledgeNetworkUserInvoice acknowledgeNetworkUserInvoice
  )
  {
    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(options.Value.AcknowledgeNetworkUserInvoice));

    await endpoint.Send(
      acknowledgeNetworkUserInvoice
    );
  }
}
