using Microsoft.EntityFrameworkCore;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities;

namespace Ozds.Business.Mutations;

public class OzdsNetworkUserInvoiceMutations(OzdsDataDbContext dbContext)
  : IOzdsMutations
{
  private readonly OzdsDataDbContext _dbContext = dbContext;

  public async Task UpdateBillId(string id, string registrationId)
  {
    await _dbContext.NetworkUserInvoices
      .Where(_dbContext.PrimaryKeyEquals<NetworkUserInvoiceEntity>(id))
      .ExecuteUpdateAsync(
        s => s
          .SetProperty(x => x.BillId, registrationId));
  }
}
