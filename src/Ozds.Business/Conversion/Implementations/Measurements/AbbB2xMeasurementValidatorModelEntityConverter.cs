using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xMeasurementValidatorModelEntityConverter :
  ConcreteModelEntityConverter<AbbB2xMeasurementValidatorModel,
    AbbB2xMeasurementValidatorEntity>
{
  protected override AbbB2xMeasurementValidatorEntity ToEntity(
    AbbB2xMeasurementValidatorModel model)
  {
    return model.ToEntity();
  }

  protected override AbbB2xMeasurementValidatorModel ToModel(
    AbbB2xMeasurementValidatorEntity entity)
  {
    return entity.ToModel();
  }
}

public static class AbbB2xMeasurementValidatorModelEntityConverterExtensions
{
  public static AbbB2xMeasurementValidatorModel ToModel(
    this AbbB2xMeasurementValidatorEntity entity)
  {
    return new AbbB2xMeasurementValidatorModel
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
      MaxReactivePower_VAR = (decimal)entity.MaxReactivePower_VAR
    };
  }

  public static AbbB2xMeasurementValidatorEntity ToEntity(
    this AbbB2xMeasurementValidatorModel model)
  {
    return new AbbB2xMeasurementValidatorEntity
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
      MaxReactivePower_VAR = (float)model.MaxReactivePower_VAR
    };
  }
}
