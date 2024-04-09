using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxMeterModelEntityConverter : ModelEntityConverter<
  SchneideriEM3xxxMeterModel, SchneideriEM3xxxMeterEntity>
{
  protected override SchneideriEM3xxxMeterEntity ToEntity(
    SchneideriEM3xxxMeterModel model)
  {
    return model.ToEntity();
  }

  protected override SchneideriEM3xxxMeterModel ToModel(
    SchneideriEM3xxxMeterEntity entity)
  {
    return entity.ToModel();
  }
}

public static class SchneideriEM3xxxMeterModelEntityConverterExtensions
{
  public static SchneideriEM3xxxMeterEntity ToEntity(
    this SchneideriEM3xxxMeterModel model)
  {
    return new SchneideriEM3xxxMeterEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      MessengerId = model.MessengerId,
      MeasurementValidatorId = model.MeasurementValidatorId,
      ConnectionPower_W = model.ConnectionPower_W,
      Phases = model.Phases.Select(phase => phase.ToEntity()).ToList()
    };
  }

  public static SchneideriEM3xxxMeterModel ToModel(
    this SchneideriEM3xxxMeterEntity entity)
  {
    return new SchneideriEM3xxxMeterModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      MessengerId = entity.MessengerId,
      MeasurementValidatorId = entity.MeasurementValidatorId,
      ConnectionPower_W = entity.ConnectionPower_W,
      Phases = entity.Phases.Select(phase => phase.ToModel()).ToList()
    };
  }
}
