using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MessengerQueries(IDbContextFactory<DataDbContext> factory)
  : IQueries
{
  public async Task<PaginatedList<MessengerEntity>> ReadByLocationId(
    string locationId,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultPageCount,
    bool deleted = false,
    string? title = null
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var filtered = context.Messengers.Where(
      context.ForeignKeyEquals<MessengerEntity>(
        nameof(MessengerEntity.Location),
        locationId
      )
    );

    filtered = deleted
      ? filtered.Where(x => x.IsDeleted)
      : filtered.Where(x => !x.IsDeleted);

    if (!string.IsNullOrWhiteSpace(title))
    {
      filtered = filtered.Where(x => x.Title.Contains(title));
    }

    var ordered = filtered.OrderBy(context.PrimaryKeyOf<MessengerEntity>());

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip(pageNumber * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items.ToPaginatedList(count);
  }

  public async Task<MessengerEntity?> ReadByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var messenger = await context
      .Meters.Where(context.PrimaryKeyEquals<MeterEntity>(meterId))
      .Include(x => x.Messenger)
      .Select(x => x.Messenger)
      .FirstOrDefaultAsync(cancellationToken);

    return messenger;
  }

  public async Task<List<MessengerEntity?>> ReadByMeterIdsOrdered(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var intermediaries = await context
      .Meters.Where(context.PrimaryKeyIn<MeterEntity>(meterIds))
      .Include(x => x.Messenger)
      .Select(x => new ReadByMeterIdsIntermediary
      {
        Meter = x,
        Messenger = x.Messenger,
      })
      .ToDictionaryAsync(x => x.Meter.Id, x => x, cancellationToken);

    return meterIds
      .Select(id =>
      {
        if (intermediaries.TryGetValue(id, out var intermediary))
        {
          return intermediary.Messenger;
        }

        return default;
      })
      .ToList();
  }

  private sealed class ReadByMeterIdsIntermediary
  {
    public required MeterEntity Meter { get; init; }

    public required MessengerEntity? Messenger { get; init; }
  }
}
