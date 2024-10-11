using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class AbortNetworkUserInvoice(
  string NetworkUserInvoiceId,
  string Reason
) : IAbortNetworkUserInvoice;
