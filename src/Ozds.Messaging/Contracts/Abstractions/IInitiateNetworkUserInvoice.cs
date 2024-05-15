using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("initiate-network-user-invoice")]
public interface IInitiateNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
}
