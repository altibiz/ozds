using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("abort-network-user-invoice")]
public interface IAbortNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
  public string Reason { get; }
}
