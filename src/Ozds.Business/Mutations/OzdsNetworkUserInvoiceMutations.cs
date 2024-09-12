using Microsoft.EntityFrameworkCore;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Extensions;

namespace Ozds.Business.Mutations;

public class OzdsNetworkUserInvoiceMutations(DataDbContext dbContext)
  : IMutations
{
  private readonly DataDbContext _dbContext = dbContext;

  public async Task UpdateBillId(string id, string registrationId)
  {
    await _dbContext.NetworkUserInvoices
      .Where(_dbContext.PrimaryKeyEquals<NetworkUserInvoiceEntity>(id))
      .ExecuteUpdateAsync(
        s => s
          .SetProperty(x => x.BillId, registrationId));
  }
}
