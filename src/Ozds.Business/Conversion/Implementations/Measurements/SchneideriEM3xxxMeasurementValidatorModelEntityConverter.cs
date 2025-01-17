using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class SchneideriEM3xxxMeasurementValidatorModelEntityConverter :
  ConcreteModelEntityConverter<SchneideriEM3xxxMeasurementValidatorModel,
    SchneideriEM3xxxMeasurementValidatorEntity>
{
  protected override SchneideriEM3xxxMeasurementValidatorEntity ToEntity(
    SchneideriEM3xxxMeasurementValidatorModel model)
  {
    return model.ToEntity();
  }

  protected override SchneideriEM3xxxMeasurementValidatorModel ToModel(
    SchneideriEM3xxxMeasurementValidatorEntity entity)
  {
    return entity.ToModel();
  }
}

public static class
  SchneideriEM3xxxMeasurementValidatorModelEntityConverterExtensions
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
      MinVoltage_V = (decimal)entity.MinVoltage_V,
      MaxVoltage_V = (decimal)entity.MaxVoltage_V,
      MinCurrent_A = (decimal)entity.MinCurrent_A,
      MaxCurrent_A = (decimal)entity.MaxCurrent_A,
      MinActivePower_W = (decimal)entity.MinActivePower_W,
      MaxActivePower_W = (decimal)entity.MaxActivePower_W,
      MinReactivePower_VAR = (decimal)entity.MinReactivePower_VAR,
      MaxReactivePower_VAR = (decimal)entity.MaxReactivePower_VAR,
      MinApparentPower_VA = (decimal)entity.MinApparentPower_VA,
      MaxApparentPower_VA = (decimal)entity.MaxApparentPower_VA
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
      MinVoltage_V = (float)model.MinVoltage_V,
      MaxVoltage_V = (float)model.MaxVoltage_V,
      MinCurrent_A = (float)model.MinCurrent_A,
      MaxCurrent_A = (float)model.MaxCurrent_A,
      MinActivePower_W = (float)model.MinActivePower_W,
      MaxActivePower_W = (float)model.MaxActivePower_W,
      MinReactivePower_VAR = (float)model.MinReactivePower_VAR,
      MaxReactivePower_VAR = (float)model.MaxReactivePower_VAR,
      MinApparentPower_VA = (float)model.MinApparentPower_VA,
      MaxApparentPower_VA = (float)model.MaxApparentPower_VA
    };
  }
}
