using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  AbbB2xMeterModelEntityConverter : ConcreteModelEntityConverter<AbbB2xMeterModel,
  AbbB2xMeterEntity>
{
  protected override AbbB2xMeterEntity ToEntity(AbbB2xMeterModel model)
  {
    return model.ToEntity();
  }

  protected override AbbB2xMeterModel ToModel(AbbB2xMeterEntity entity)
  {
    return entity.ToModel();
  }
}

public static class AbbB2xMeterModelEntityConverterExtensions
{
  public static AbbB2xMeterEntity ToEntity(this AbbB2xMeterModel model)
  {
    return new AbbB2xMeterEntity
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
      ConnectionPower_W = (float)model.ConnectionPower_W,
      Phases = model.Phases.Select(p => p.ToEntity()).ToList(),
      Kind = model.Kind
    };
  }

  public static AbbB2xMeterModel ToModel(this AbbB2xMeterEntity entity)
  {
    return new AbbB2xMeterModel
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
      ConnectionPower_W = (decimal)entity.ConnectionPower_W,
      Phases = entity.Phases.Select(p => p.ToModel()).ToHashSet(),
      Kind = entity.Kind
    };
  }
}
