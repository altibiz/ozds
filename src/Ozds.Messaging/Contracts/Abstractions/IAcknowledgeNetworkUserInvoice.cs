using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("acknowledge-network-user-invoice")]
public interface IAcknowledgeNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
}
