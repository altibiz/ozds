using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Queries;
using DataMeasurementUpsertMutations = Ozds.Data.Mutations.MeasurementUpsertMutations;

namespace Ozds.Business.Mutations;

public record UpsertMeasurementsResult(
  List<IMeasurement> Measurements,
  List<ValidationResult> ValidationResults
);

public class MeasurementUpsertMutations(
  DataMeasurementUpsertMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  ValidationQueries validationQueries,
  AgnosticMeasurementAggregateConverter aggregateConverter,
  AgnosticAggregateUpserter aggregateUpserter
)
{
  public async Task<UpsertMeasurementsResult> UpsertMeasurements(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    if (await Validate(measurements) is { } validationResults)
    {
      return new UpsertMeasurementsResult(
        measurements.ToList(),
        validationResults
      );
    }

    var aggregates = measurements
      .OfType<IAggregate>()
      .SelectMany(x => Enum.GetValues<IntervalModel>()
        .Select(interval => aggregateConverter.ToAggregate(x, interval)))
      .GroupBy(x => new
      {
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x.Interval
      })
      .Select(x => x.Aggregate(aggregateUpserter.UpsertModelAgnostic))
      .ToList();

    foreach (var x in aggregates)
    {
      var key = new UpsertAggregateCacheKey(
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp);

      UpsertAggregateCache
        .AddOrUpdate(
          key,
          _ => new List<IAggregate>() { x },
          (_, list) =>
          {
            list.Add(x);
            return list;
          });
    }

    var flushed = FlushCacheInternal(aggregates);

    var entities = measurements
      .Concat(flushed)
      .Select(modelEntityConverter.ToEntity<IMeasurementEntity>);

    var result = await mutations.UpsertMeasurements(
      entities,
      cancellationToken
    );

    var models = result
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();

    return new UpsertMeasurementsResult(
      models,
      new()
    );
  }

  public async Task<List<IMeasurement>> FlushCache(
    CancellationToken cancellationToken,
    IEnumerable<IAggregate>? aggregates = null
  )
  {
    var measurements = FlushCacheInternal(aggregates);

    var entities = measurements
      .Select(modelEntityConverter.ToEntity<IMeasurementEntity>);

    var result = await mutations.UpsertMeasurements(
      entities,
      cancellationToken
    );

    var models = result
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();

    return models;
  }

  private List<IMeasurement> FlushCacheInternal(
    IEnumerable<IAggregate>? aggregates = null
  )
  {
    var cacheCopy = UpsertAggregateCache
      .ToDictionary(
        x => x.Key,
        x => x.Value.ToList());
    if (aggregates is null)
    {
      var all = new List<IMeasurement>();
      foreach (var cached in cacheCopy)
      {
        if (UpsertAggregateCache.TryRemove(cached.Key, out var value))
        {
          var upserted = value
            .Aggregate(aggregateUpserter.UpsertModelAgnostic);
          all.Add(upserted);
        }
      }
      return all;
    }

    var some = new List<IMeasurement>();
    foreach (var cached in cacheCopy)
    {
      var upserted = cached.Value
        .Aggregate(aggregateUpserter.UpsertModelAgnostic);
      if (!aggregates.Any(aggregate =>
        aggregate.MeterId == upserted.MeterId
        && aggregate.MeasurementLocationId == upserted.MeasurementLocationId
        && aggregate.Timestamp == upserted.Timestamp
        && aggregate.Interval == upserted.Interval)
        && UpsertAggregateCache.TryRemove(cached.Key, out var value))
      {
        upserted = value
          .Aggregate(aggregateUpserter.UpsertModelAgnostic);
        some.Add(upserted);
      }
    }
    return some;
  }

  private async Task<List<ValidationResult>?> Validate(
    IEnumerable<IMeasurement> measurements
  )
  {
    var meterIds = measurements.Select(x => x.MeterId)
      .Distinct()
      .ToList();

    var validators = await validationQueries.ReadMeasurementValidatorByMeters(
      meterIds,
      CancellationToken.None
    );

    var validationResults = new List<ValidationResult>();
    foreach (var validationMeasurement in measurements)
    {
      var validator = validators
        .FirstOrDefault(x => x.Id == validationMeasurement.MeterId);
      if (validator is null)
      {
        continue;
      }

      var validationContext = new ValidationContext(validationMeasurement)
      {
        Items = { ["MeasurementValidator"] = validator }
      };

      validationResults.AddRange(
        validationMeasurement
          .Validate(validationContext));
    }

    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    return null;
  }

  private static readonly ConcurrentDictionary<
    UpsertAggregateCacheKey,
    List<IAggregate>> UpsertAggregateCache =
      new ConcurrentDictionary<
        UpsertAggregateCacheKey,
        List<IAggregate>>();

  private sealed record UpsertAggregateCacheKey(
    string MeterId,
    string MeasurementLocationId,
    DateTimeOffset Timestamp
  );
}
