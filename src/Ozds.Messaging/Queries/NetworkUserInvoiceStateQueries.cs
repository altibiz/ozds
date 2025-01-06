using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Queries.Abstractions;

namespace Ozds.Messaging.Queries;

public class NetworkUserInvoiceStateQueries(
  IDbContextFactory<MessagingDbContext> factory
) : IQueries
{
  public async Task<NetworkUserInvoiceStateEntity?> ReadAsync(
    string networkUserInvoiceId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    return await context.NetworkUserInvoiceStates
      .Where(x => x.NetworkUserInvoiceId == networkUserInvoiceId)
      .FirstOrDefaultAsync(cancellationToken);
  }
}
