using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class InitiateNetworkUserInvoice(
  string NetworkUserInvoiceId
) : IInitiateNetworkUserInvoice;
