using System.Diagnostics;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataMeasurementMutations = Ozds.Data.Mutations.MeasurementMutations;

namespace Ozds.Business.Mutations;

public class MeasurementMutations(
  DataMeasurementMutations mutations,
  ModelEntityConverter modelEntityConverter,
  ILogger<MeasurementMutations> logger
) : IMutations
{
  public async Task DeleteOlderThan(
    DateTimeOffset threshold,
    CancellationToken cancellationToken
  )
  {
    await mutations.DeleteOlderThan(
      threshold,
      cancellationToken
    );
  }

  public async Task<List<IMeasurement>> Create(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken,
    bool triggerEvents = true
  )
  {
    var entities = modelEntityConverter
      .ToEntities<IMeasurementEntity>(measurements);

    var stopwatch = Stopwatch.StartNew();
    var result = await mutations.Create(
      entities,
      cancellationToken,
      triggerEvents
    );
    stopwatch.Stop();
    logger.LogDebug(
      "Upserted {Count} measurements in {Elapsed}",
      result.Count,
      stopwatch.Elapsed);

    var models = modelEntityConverter
      .ToModels<IMeasurement>(result)
      .ToList();

    return models;
  }

  public async Task<List<IMeasurement>> Create(
    IAsyncEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken,
    bool triggerEvents = true
  )
  {
    var entities = modelEntityConverter
      .ToEntities<IMeasurementEntity>(
        measurements,
        cancellationToken);

    var stopwatch = Stopwatch.StartNew();
    var result = await mutations.Create(
      entities,
      cancellationToken,
      triggerEvents
    );
    stopwatch.Stop();
    logger.LogDebug(
      "Upserted {Count} measurements in {Elapsed}",
      result.Count,
      stopwatch.Elapsed);

    var models = modelEntityConverter
      .ToModels<IMeasurement>(result)
      .ToList();

    return models;
  }
}
