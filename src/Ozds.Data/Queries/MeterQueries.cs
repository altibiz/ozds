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
}
