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

  public async Task<List<IMeasurementValidatorEntity>> ReadMeasurementValidatorByMeters(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    return await context.Meters
      .Where(context.PrimaryKeyIn<MeterEntity>(meterIds))
      .Include(x => x.MeasurementValidator)
      .Select(x => x.MeasurementValidator)
      .OfType<IMeasurementValidatorEntity>()
      .ToListAsync(cancellationToken);
  }

  public async Task<IMeterEntity?> ReadMeterByMeasurementValidator(
    string validatorId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    return await context.Meters
      .Where(context.ForeignKeyEquals<MeterEntity>(
        nameof(MeterEntity<MeasurementEntity, AggregateEntity, MeasurementValidatorEntity>.MeasurementValidator),
        validatorId))
      .OfType<IMeterEntity>()
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<List<IMeterEntity>> ReadMetersByMeasurementValidators(
    IEnumerable<string> validatorIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    return await context.Meters
      .Where(context.ForeignKeyIn<MeterEntity>(
        nameof(MeterEntity<MeasurementEntity, AggregateEntity, MeasurementValidatorEntity>.MeasurementValidator),
        validatorIds))
      .OfType<IMeterEntity>()
      .ToListAsync(cancellationToken);
  }
}
