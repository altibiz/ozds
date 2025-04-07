using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class ApproveNetworkUserInvoice(
  string NetworkUserInvoiceId,
  bool Approved
) : IApproveNetworkUserInvoice;
