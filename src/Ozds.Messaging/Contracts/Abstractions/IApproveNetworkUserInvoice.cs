using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("approve-network-user-invoice")]
public interface IApproveNetworkUserInvoice : INetworkUserInvoiceCommand
{
  public bool Approved { get; }
}
