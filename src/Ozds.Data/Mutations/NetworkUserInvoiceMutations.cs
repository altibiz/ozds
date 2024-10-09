using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Business.Mutations;

public class NetworkUserInvoiceMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task UpdateBillId(
    string id,
    string registrationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    await context.NetworkUserInvoices
      .Where(context.PrimaryKeyEquals<NetworkUserInvoiceEntity>(id))
      .ExecuteUpdateAsync(
        s => s.SetProperty(x => x.BillId, registrationId),
        cancellationToken);
  }
}
