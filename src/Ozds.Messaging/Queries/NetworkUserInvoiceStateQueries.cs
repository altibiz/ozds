using Microsoft.EntityFrameworkCore;
using Ozds.Messaging.Context;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Queries.Abstractions;

namespace Ozds.Messaging.Queries;

public class NetworkUserInvoiceStateQueries(
  MessagingDbContext context
) : IQueries
{
  public async Task<NetworkUserInvoiceStateEntity?> ReadAsync(
    string networkUserInvoiceId
  )
  {
    return await context.NetworkUserInvoiceStates
      .Where(x => x.NetworkUserInvoiceId == networkUserInvoiceId)
      .FirstOrDefaultAsync();
  }
}
