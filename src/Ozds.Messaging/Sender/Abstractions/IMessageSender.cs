using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Sender.Abstractions;

public interface IMessageSender
{
  public Task AcknowledgeNetworkUserInvoice(
    IAcknowledgeNetworkUserInvoice acknowledgeNetworkUserInvoice,
    CancellationToken cancellationToken);

  public Task AcknowledgeNetworkUserInvoices(
    IEnumerable<IAcknowledgeNetworkUserInvoice> acknowledgeNetworkUserInvoices,
    CancellationToken cancellationToken);
}
