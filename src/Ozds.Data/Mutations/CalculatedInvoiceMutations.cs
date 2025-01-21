using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Mutations.Abstractions;

namespace Ozds.Data.Mutations;

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

    try
    {
      await using var transaction = await context.Database
        .BeginTransactionAsync(cancellationToken);

      context.Add(invoice.Invoice);
      await context.SaveChangesAsync(cancellationToken);

      foreach (var calculation in invoice.Calculations)
      {
        calculation.NetworkUserInvoiceId = invoice.Invoice.Id;
        context.Add(calculation);
      }
      if (invoice.Calculations.Count > 0)
      {
        await context.SaveChangesAsync(cancellationToken);
      }

      await context.Database.CommitTransactionAsync(cancellationToken);
    }
    catch
    {
      if (context.Database.CurrentTransaction is { } transaction)
      {
        await transaction.RollbackAsync(cancellationToken);
      }

      throw;
    }
  }
}
