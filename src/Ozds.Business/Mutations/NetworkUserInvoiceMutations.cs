using Ozds.Data.Mutations.Abstractions;
using DataNetworkUserInvoiceMutations =
  Ozds.Data.Mutations.NetworkUserInvoiceMutations;

namespace Ozds.Business.Mutations;

public class NetworkUserInvoiceMutations(
  DataNetworkUserInvoiceMutations mutations
) : IMutations
{
  public async Task UpdateBillId(
    string id,
    string registrationId,
    CancellationToken cancellationToken
  )
  {
    await mutations.UpdateBillId(id, registrationId, cancellationToken);
  }
}
