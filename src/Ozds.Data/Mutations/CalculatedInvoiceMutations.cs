using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

// TODO: in one transaction

public class CalculatedInvoiceMutations(
  IDbContextFactory<DataDbContext> factory
) : IMutations
{
  public async Task CreateCalculatedInvoice(
    CalculatedNetworkUserInvoiceEntity invoice,
    CancellationToken cancellationToken)
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    context.Add(invoice.Invoice);
    await context.SaveChangesAsync(cancellationToken);

    foreach (var calculation in invoice.Calculations)
    {
      calculation.NetworkUserInvoiceId = invoice.Invoice.Id;
      context.Add(calculation);
    }
    await context.SaveChangesAsync(cancellationToken);
  }
}
