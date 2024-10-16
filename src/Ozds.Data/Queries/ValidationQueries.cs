using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class ValidationQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<IMeasurementValidatorEntity?> ReadMeasurementValidatorByMeter(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    return await context.Meters
      .Where(context.PrimaryKeyEquals<MeterEntity>(meterId))
      .Include(x => x.MeasurementValidator)
      .Select(x => x.MeasurementValidator)
      .FirstOrDefaultAsync(cancellationToken);
  }
}
