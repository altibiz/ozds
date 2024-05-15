using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class AcknowledgeNetworkUserInvoice(
  string NetworkUserInvoiceId
) : IAcknowledgeNetworkUserInvoice;
