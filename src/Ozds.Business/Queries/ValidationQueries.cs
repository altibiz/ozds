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
  public async Task<IMeasurementValidator?> ReadMeasurementValidatorByMeter(
    string meterId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeasurementValidatorByMeter(
      meterId,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    var model = modelEntityConverter.ToModel<IMeasurementValidator>(entity);

    return model;
  }

  public async Task<List<IMeasurementValidator>>
    ReadMeasurementValidatorsByMeters(
      IEnumerable<string> meterIds,
      CancellationToken cancellationToken
    )
  {
    var entities = await queries.ReadMeasurementValidatorByMeters(
      meterIds,
      cancellationToken);

    var models = entities
      .Select(modelEntityConverter.ToModel<IMeasurementValidator>)
      .ToList();

    return models;
  }

  public async Task<IMeter?> ReadMeterByMeasurementValidator(
    string validatorId,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadMeterByMeasurementValidator(
      validatorId,
      cancellationToken);
    if (entity is null)
    {
      return null;
    }

    var model = modelEntityConverter.ToModel<IMeter>(entity);
    return model;
  }

  public async Task<List<IMeter>> ReadMetersByMeasurementValidators(
    IEnumerable<string> validatorIds,
    CancellationToken cancellationToken
  )
  {
    var entities = await queries.ReadMetersByMeasurementValidators(
      validatorIds,
      cancellationToken);

    var models = entities
      .Select(modelEntityConverter.ToModel<IMeter>)
      .ToList();

    return models;
  }
}
