using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MessengerQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<MessengerEntity>> ReadByLocationId(
    string locationId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.Messengers
      .Where(context.ForeignKeyEquals<MessengerEntity>(
        nameof(MessengerEntity.Location),
        locationId));

    var ordered = filtered
      .OrderBy(context.PrimaryKeyOf<MessengerEntity>());

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }
}
