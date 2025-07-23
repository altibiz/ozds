using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class MeasurementLocationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<IMeasurementLocationEntity?> ReadByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var measurementLocation = await context
      .MeasurementLocations.Where(
        context.ForeignKeyEquals<MeasurementLocationEntity>(
          nameof(MeasurementLocationEntity.Meter),
          meterId
        )
      )
      .FirstOrDefaultAsync(cancellationToken);
    return measurementLocation;
  }

  public async Task<List<IMeasurementLocationEntity?>> ReadByMeterIdsOrdered(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var intermediaries = await context
      .MeasurementLocations.Where(
        context.ForeignKeyIn<MeasurementLocationEntity>(
          nameof(MeasurementLocationEntity.Meter),
          meterIds
        )
      )
      .Select(
        x => new ReadByMeterIdsIntermediary
        {
          Meter = x.Meter,
          MeasurementLocation = x
        })
      .ToDictionaryAsync(
        x => x.Meter.Id,
        x => x,
        cancellationToken);

    return meterIds
      .Select(
        id =>
        {
          if (intermediaries.TryGetValue(id, out var intermediary))
          {
            return intermediary.MeasurementLocation;
          }

          return default;
        })
      .Cast<IMeasurementLocationEntity?>()
      .ToList();
  }

  public async Task<
      List<IMeasurementLocationEntity>>
    ReadByNetworkUserId(
      string networkUserId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    var measurementLocations = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUserId
        )
      )
      .Select(x => (IMeasurementLocationEntity)x)
      .ToListAsync(cancellationToken);
    return measurementLocations;
  }

  public async Task<List<IMeasurementLocationEntity>>
    ReadByLocationId(
      string locationId,
      CancellationToken cancellationToken
    )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var networkUsers = await context
      .NetworkUsers.OfType<NetworkUserEntity>()
      .Where(
        context.ForeignKeyEquals<NetworkUserEntity>(
          nameof(NetworkUserEntity.Location),
          locationId
        )
      )
      .ToListAsync(cancellationToken);

    var measurementLocations = await context
      .MeasurementLocations.OfType<NetworkUserMeasurementLocationEntity>()
      .Where(
        context.ForeignKeyIn<NetworkUserMeasurementLocationEntity>(
          nameof(NetworkUserMeasurementLocationEntity.NetworkUser),
          networkUsers.Select(x => x.Id).ToList()
        )
      )
      .Select(x => (IMeasurementLocationEntity)x)
      .ToListAsync(cancellationToken);

    return measurementLocations;
  }

  private sealed class ReadByMeterIdsIntermediary
  {
    public required MeterEntity Meter { get; init; }

    public required MeasurementLocationEntity MeasurementLocation { get; init; }
  }
}
