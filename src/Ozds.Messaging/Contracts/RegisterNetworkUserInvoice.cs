using Ozds.Messaging.Contracts.Abstractions;

namespace Ozds.Messaging.Contracts;

public record class RegisterNetworkUserInvoice(
  string NetworkUserInvoiceId,
  string BillId
) : IRegisterNetworkUserInvoice;
