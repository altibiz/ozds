using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("cancel-network-user-invoice")]
public interface ICancelNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
}
