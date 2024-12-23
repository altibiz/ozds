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
  public async Task<List<IMeasurementLocationEntity>> ReadByMeters(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var measurementLocations = await context.MeasurementLocations
      .Where(context.ForeignKeyIn<MeasurementLocationEntity>(
        nameof(MeasurementLocationEntity.Meter),
        meterIds))
      .ToListAsync(cancellationToken);
    return measurementLocations
      .OfType<IMeasurementLocationEntity>()
      .Distinct()
      .ToList();
  }
}
