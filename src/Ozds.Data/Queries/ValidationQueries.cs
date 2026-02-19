using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class ValidationQueries(IDbContextFactory<DataDbContext> factory)
  : IQueries
{
  public async Task<IMeasurementValidatorEntity?> ReadMeasurementValidatorByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    return await context
      .Meters.Where(context.PrimaryKeyEquals<MeterEntity>(meterId))
      .Include(x => x.MeasurementValidator)
      .Select(x => x.MeasurementValidator)
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<
    List<IMeasurementValidatorEntity?>
  > ReadMeasurementValidatorsByMeterIdsOrdered(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var intermediaries = await context
      .Meters.Where(context.PrimaryKeyIn<MeterEntity>(meterIds))
      .Include(x => x.MeasurementValidator)
      .Select(x => new ReadMeasurementValidatorsByMeterIdsIntermediary
      {
        Meter = x,
        MeasurementValidator = x.MeasurementValidator,
      })
      .ToDictionaryAsync(x => x.Meter.Id, x => x, cancellationToken);

    return meterIds
      .Select(id =>
      {
        if (intermediaries.TryGetValue(id, out var intermediary))
        {
          return intermediary.MeasurementValidator;
        }

        return default;
      })
      .Cast<IMeasurementValidatorEntity?>()
      .ToList();
  }

  public async Task<IMeterEntity?> ReadMeterByMeasurementValidatorId(
    string validatorId,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );
    return await context
      .Meters.Where(
        context.ForeignKeyEquals<MeterEntity>(
          nameof(
            MeterEntity<
              MeasurementEntity,
              AggregateEntity,
              MeasurementValidatorEntity
            >.MeasurementValidator
          ),
          validatorId
        )
      )
      .OfType<IMeterEntity>()
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<
    List<IMeterEntity?>
  > ReadMetersByMeasurementValidatorIdsOrdered(
    IEnumerable<string> validatorIds,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    var intermediaries = await context
      .Meters.Where(
        context.ForeignKeyIn<MeterEntity>(
          nameof(
            MeterEntity<
              MeasurementEntity,
              AggregateEntity,
              MeasurementValidatorEntity
            >.MeasurementValidator
          ),
          validatorIds
        )
      )
      .Select(
        context
          .ForeignKeyOf<MeterEntity>(
            nameof(
              MeterEntity<
                MeasurementEntity,
                AggregateEntity,
                MeasurementValidatorEntity
              >.MeasurementValidator
            )
          )
          .Suffix(meter => new ReadMetersByMeasurementValidatorIdsInterMediary
          {
            Meter = (meter as MeterEntity)!,
            MeasurementValidatorId = (
              meter as MeterEntity
            )!.MeasurementValidatorId,
          })
      )
      .ToDictionaryAsync(
        x => x.MeasurementValidatorId,
        x => x,
        cancellationToken
      );

    return validatorIds
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

  private sealed class ReadMeasurementValidatorsByMeterIdsIntermediary
  {
    public required MeterEntity Meter { get; init; }

    public required MeasurementValidatorEntity MeasurementValidator { get; init; }
  }

  private sealed class ReadMetersByMeasurementValidatorIdsInterMediary
  {
    public required MeterEntity Meter { get; init; }

    public required string MeasurementValidatorId { get; init; }
  }
}
