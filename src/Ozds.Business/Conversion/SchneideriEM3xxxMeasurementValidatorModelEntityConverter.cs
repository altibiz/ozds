using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxMeasurementValidatorModelEntityConverter : ModelEntityConverter<SchneideriEM3xxxMeasurementValidatorModel, SchneideriEM3xxxMeasurementValidatorEntity>
{
  protected override SchneideriEM3xxxMeasurementValidatorEntity ToEntity(SchneideriEM3xxxMeasurementValidatorModel model) =>
    model.ToEntity();

  protected override SchneideriEM3xxxMeasurementValidatorModel ToModel(SchneideriEM3xxxMeasurementValidatorEntity entity) =>
    entity.ToModel();
}

public static class SchneideriEM3xxxMeasurementValidatorModelEntityConverterExtensions
{
  public static SchneideriEM3xxxMeasurementValidatorModel ToModel(
    this SchneideriEM3xxxMeasurementValidatorEntity entity)
  {
    return new SchneideriEM3xxxMeasurementValidatorModel
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
      MinVoltage_V = entity.MinVoltage_V,
      MaxVoltage_V = entity.MaxVoltage_V,
      MinCurrent_A = entity.MinCurrent_A,
      MaxCurrent_A = entity.MaxCurrent_A,
      MinActivePower_W = entity.MinActivePower_W,
      MaxActivePower_W = entity.MaxActivePower_W,
      MinReactivePower_VAR = entity.MinReactivePower_VAR,
      MaxReactivePower_VAR = entity.MaxReactivePower_VAR,
      MinApparentPower_VA = entity.MinApparentPower_VA,
      MaxApparentPower_VA = entity.MaxApparentPower_VA
    };
  }

  public static SchneideriEM3xxxMeasurementValidatorEntity ToEntity(
    this SchneideriEM3xxxMeasurementValidatorModel model)
  {
    return new SchneideriEM3xxxMeasurementValidatorEntity
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
      MinVoltage_V = model.MinVoltage_V,
      MaxVoltage_V = model.MaxVoltage_V,
      MinCurrent_A = model.MinCurrent_A,
      MaxCurrent_A = model.MaxCurrent_A,
      MinActivePower_W = model.MinActivePower_W,
      MaxActivePower_W = model.MaxActivePower_W,
      MinReactivePower_VAR = model.MinReactivePower_VAR,
      MaxReactivePower_VAR = model.MaxReactivePower_VAR,
      MinApparentPower_VA = model.MinApparentPower_VA,
      MaxApparentPower_VA = model.MaxApparentPower_VA
    };
  }
}
