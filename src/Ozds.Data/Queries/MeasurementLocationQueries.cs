using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
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
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var measurementLocation = await context.MeasurementLocations
      .Where(
        context.ForeignKeyEquals<MeasurementLocationEntity>(
          nameof(MeasurementLocationEntity.Meter),
          meterId))
      .FirstOrDefaultAsync(cancellationToken);
    return measurementLocation;
  }

  public async Task<IMeterEntity?> ReadMeterByMeasurementLocation(
    string measurementLocationId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var meter = await context.MeasurementLocations
      .Where(
        context.PrimaryKeyEquals<MeasurementLocationEntity>(
          measurementLocationId))
      .Include(x => x.Meter)
      .Select(x => x.Meter)
      .FirstOrDefaultAsync(cancellationToken);
    return meter;
  }
}
