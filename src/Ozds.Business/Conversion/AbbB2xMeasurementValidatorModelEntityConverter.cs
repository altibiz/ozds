using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class AbbB2xMeasurementValidatorModelEntityConverter : ModelEntityConverter<AbbB2xMeasurementValidatorModel, AbbB2xMeasurementValidatorEntity>
{
  protected override AbbB2xMeasurementValidatorEntity ToEntity(AbbB2xMeasurementValidatorModel model) =>
    model.ToEntity();

  protected override AbbB2xMeasurementValidatorModel ToModel(AbbB2xMeasurementValidatorEntity entity) =>
    entity.ToModel();
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
      MinVoltage_V = entity.MinVoltage_V,
      MaxVoltage_V = entity.MaxVoltage_V,
      MinCurrent_A = entity.MinCurrent_A,
      MaxCurrent_A = entity.MaxCurrent_A,
      MinActivePower_W = entity.MinActivePower_W,
      MaxActivePower_W = entity.MaxActivePower_W,
      MinReactivePower_VAR = entity.MinReactivePower_VAR,
      MaxReactivePower_VAR = entity.MaxReactivePower_VAR
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
      MinVoltage_V = model.MinVoltage_V,
      MaxVoltage_V = model.MaxVoltage_V,
      MinCurrent_A = model.MinCurrent_A,
      MaxCurrent_A = model.MaxCurrent_A,
      MinActivePower_W = model.MinActivePower_W,
      MaxActivePower_W = model.MaxActivePower_W,
      MinReactivePower_VAR = model.MinReactivePower_VAR,
      MaxReactivePower_VAR = model.MaxReactivePower_VAR
    };
  }
}
