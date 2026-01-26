using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using DataValidationQueries = Ozds.Data.Queries.ValidationQueries;

namespace Ozds.Business.Queries;

public class ValidationQueries(
  DataValidationQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<IMeasurementValidator?> ReadMeasurementValidatorByMeterId(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeasurementValidatorByMeterId(
      meterId,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    var model = modelEntityConverter.ToModel<IMeasurementValidator>(entity);

    return model;
  }

  public async Task<List<IMeasurementValidator?>>
    ReadMeasurementValidatorsByMeterIdsOrdered(
      IEnumerable<string> meterIds,
      CancellationToken cancellationToken
    )
  {
    var entities = await queries.ReadMeasurementValidatorsByMeterIdsOrdered(
      meterIds,
      cancellationToken);

    var models = entities
      .Select(entity => entity is null
        ? null
        : modelEntityConverter.ToModel<IMeasurementValidator>(entity))
      .ToList();

    return models;
  }

  public async Task<IMeter?> ReadMeterByMeasurementValidatorId(
    string validatorId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeterByMeasurementValidatorId(
      validatorId,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    var model = modelEntityConverter.ToModel<IMeter>(entity);
    return model;
  }

  public async Task<List<IMeter?>> ReadMetersByMeasurementValidatorIdsOrdered(
    IEnumerable<string> validatorIds,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadMetersByMeasurementValidatorIdsOrdered(
      validatorIds,
      cancellationToken);

    var models = entities
      .Select(entity => entity is null
        ? null
        : modelEntityConverter.ToModel<IMeter>(entity))
      .ToList();

    return models;
  }
}
