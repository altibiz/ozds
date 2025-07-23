using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MeterQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<IMeterEntity?> ReadByMeasurementLocationId(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var meter = await context
      .MeasurementLocations.Where(
        context.PrimaryKeyEquals<MeasurementLocationEntity>(
          measurementLocationId
        )
      )
      .Include(x => x.Meter)
      .Select(x => x.Meter)
      .FirstOrDefaultAsync(cancellationToken);
    return meter;
  }

  public async Task<List<IMeterEntity?>> ReadByMeasurementLocationIdsOrdered(
    IEnumerable<string> measurementLocationIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var intermediaries = await context
      .MeasurementLocations.Where(
        context.PrimaryKeyIn<MeasurementLocationEntity>(
          measurementLocationIds
        )
      )
      .Include(x => x.Meter)
      .Select(x => new ReadByMeasurementLocationIdsIntermediary
      {
        MeasurementLocation = x,
        Meter = x.Meter,
      })
      .ToDictionaryAsync(
        x => x.MeasurementLocation.Id,
        x => x,
        cancellationToken);

    return measurementLocationIds
      .Select(id =>
      {
        if (intermediaries.TryGetValue(id, out var intermediary))
        {
          return intermediary.Meter;
        }

        return default;
      })
      .Cast<IMeterEntity?>()
      .ToList();
  }

  public async Task<IMeterEntity?> ReadByMessengerId(
    string messengerId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var meter = await context.Meters
      .Where(
        context.ForeignKeyEquals<MeterEntity>(
          nameof(MeterEntity.Messenger),
          messengerId
        )
      )
      .FirstOrDefaultAsync(cancellationToken);
    return meter;
  }

  public async Task<List<IMeterEntity>> ReadByMessengerIdsOrdered(
    IEnumerable<string> messengerIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var meters = await context.Meters
      .Where(
        context.ForeignKeyIn<MeterEntity>(
          nameof(MeterEntity.Messenger),
          messengerIds
        )
      )
      .OfType<IMeterEntity>()
      .ToListAsync(cancellationToken);

    var intermediaries = await context.Meters
      .Where(
        context.ForeignKeyIn<MeterEntity>(
          nameof(MeterEntity.Messenger),
          messengerIds
        )
      )
      .Include(x => x.Messenger)
      .Select(x => new ReadByMessengerIdsIntermediary
      {
        // NOTE: id has to be one of the provided ones
        Messenger = x.Messenger!,
        Meter = x
      })
      .ToDictionaryAsync(
        x => x.Messenger.Id,
        x => x,
        cancellationToken);

    return meters
      .Select(meter =>
      {
        if (intermediaries.TryGetValue(meter.Id, out var intermediary))
        {
          return intermediary.Messenger;
        }

        return default;
      })
      .Cast<IMeterEntity>()
      .ToList();
  }

  private sealed class ReadByMeasurementLocationIdsIntermediary
  {
    public required MeasurementLocationEntity MeasurementLocation { get; init; }

    public required MeterEntity Meter { get; init; }
  }

  private sealed class ReadByMessengerIdsIntermediary
  {
    public required MessengerEntity Messenger { get; init; }

    public required MeterEntity Meter { get; init; }
  }
}
