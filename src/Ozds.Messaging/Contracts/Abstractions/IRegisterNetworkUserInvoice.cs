using MassTransit;

namespace Ozds.Messaging.Contracts.Abstractions;

[MessageUrn("register-network-user-invoice")]
public interface IRegisterNetworkUserInvoice
  : INetworkUserInvoiceCommand
{
  public string BillId { get; }
}
