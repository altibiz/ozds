using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Reflection;

namespace Ozds.Data.Mutations;

public class NetworkUserInvoiceMutations(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector
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

  // NOTE: returns whether created or already existing
  public async Task<bool> CreateCalculatedInvoice(
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

      return true;
    }
    // NOTE: unique index violation on invoice
    catch (DbUpdateException ex) when (
      ex.InnerException is PostgresException pgEx &&
      pgEx.SqlState == "23505" &&
      pgEx.TableName == reflector.ResolveEntityTable(invoice.Invoice.GetType()))
    {
      if (context.Database.CurrentTransaction is { } transaction)
      {
        await transaction.RollbackAsync(cancellationToken);
      }

      return false;
    }
    catch (Exception)
    {
      if (context.Database.CurrentTransaction is { } transaction)
      {
        await transaction.RollbackAsync(cancellationToken);
      }

      throw;
    }
  }
}
