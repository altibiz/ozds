using System.Diagnostics;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataMeasurementUpsertMutations = Ozds.Data.Mutations.MeasurementUpsertMutations;

namespace Ozds.Business.Mutations;

public class AggregateUpsertMutations(
  DataMeasurementUpsertMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  ILogger<AggregateUpsertMutations> logger
) : IMutations
{
  public async Task<List<IAggregate>> UpsertAggregates(
    IEnumerable<IAggregate> aggregates,
    CancellationToken cancellationToken
  )
  {
    aggregates = aggregates.ToList();

    var entities = aggregates
      .Select(modelEntityConverter.ToEntity<IAggregateEntity>)
      .ToList();

    logger.LogDebug(
      "Upserting {Count} aggregates...",
      entities.Count);
    var stopwatch = Stopwatch.StartNew();
    var result = await mutations.UpsertMeasurements(
      entities,
      cancellationToken
    );
    stopwatch.Stop();
    logger.LogDebug(
      "Upserted {Count} aggregates in {Elapsed}",
      entities.Count,
      stopwatch.Elapsed);

    var models = result
      .Select(modelEntityConverter.ToModel<IAggregate>)
      .ToList();

    return models;
  }
}
