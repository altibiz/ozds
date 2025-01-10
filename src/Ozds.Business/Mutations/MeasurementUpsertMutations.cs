using System.Diagnostics;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataMeasurementUpsertMutations = Ozds.Data.Mutations.MeasurementMutations;

namespace Ozds.Business.Mutations;

public class MeasurementMutations(
  DataMeasurementUpsertMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  ILogger<MeasurementMutations> logger
) : IMutations
{
  public async Task<List<IMeasurement>> CreateMeasurements(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    measurements = measurements.ToList();

    var entities = measurements
      .Select(modelEntityConverter.ToEntity<IMeasurementEntity>)
      .ToList();

    logger.LogDebug(
      "Upserting {Count} measurements...",
      entities.Count);
    var stopwatch = Stopwatch.StartNew();
    var result = await mutations.CreateMeasurements(
      entities,
      cancellationToken
    );
    stopwatch.Stop();
    logger.LogDebug(
      "Upserted {Count} measurements in {Elapsed}",
      entities.Count,
      stopwatch.Elapsed);

    var models = result
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();

    return models;
  }
}
