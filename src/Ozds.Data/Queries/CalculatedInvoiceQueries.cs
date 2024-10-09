using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

// TODO: location invoices

namespace Ozds.Data.Queries;

public class OzdsCalculatedInvoiceQueries(
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

    return new CalculatedNetworkUserInvoiceEntity(
      invoice.NetworkUserCalculations.ToList(),
      invoice
    );
  }

  public async Task<CalculatedLocationInvoiceEntity?>
    ReadCalculatedLocationInvoice(
      string id,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var invoice = await context.LocationInvoices
      .Where(context.PrimaryKeyEquals<LocationInvoiceEntity>(id))
      .FirstOrDefaultAsync();
    if (invoice is null)
    {
      return null;
    }

    return new CalculatedLocationInvoiceEntity(invoice);
  }
}
