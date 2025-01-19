using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class CalculatedInvoiceQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<CalculatedNetworkUserInvoiceEntity?>
    ReadCalculatedNetworkUserInvoice(
      string id,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var invoice = await context.NetworkUserInvoices
      .Where(context.PrimaryKeyEquals<NetworkUserInvoiceEntity>(id))
      .Include(invoice => invoice.NetworkUserCalculations)
      .FirstOrDefaultAsync();
    if (invoice is null)
    {
      return null;
    }

    return new CalculatedNetworkUserInvoiceEntity()
    {
      Calculations = invoice.NetworkUserCalculations.ToList(),
      Invoice = invoice
    };
  }
}
