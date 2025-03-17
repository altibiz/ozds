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
  public async Task<IMeasurementLocationEntity?> ReadMeasurementLocationByMeter(
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

  public async Task<IMeterEntity?> ReadMeterByMeasurementLocation(
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

  public async Task<
      List<IMeasurementLocationEntity>>
    ReadMeasurementLocationByNetworkUser(
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
    ReadMeasurementLocationByLocation(
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
}
