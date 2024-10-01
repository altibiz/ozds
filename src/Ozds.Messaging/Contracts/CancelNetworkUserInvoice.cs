using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class CancelNetworkUserInvoice(
  string NetworkUserInvoiceId,
  string Reason
) : ICancelNetworkUserInvoice;
