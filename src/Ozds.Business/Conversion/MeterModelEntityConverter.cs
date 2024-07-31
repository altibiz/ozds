using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

// TODO: this is so dirty

namespace Ozds.Business.Conversion;

public class MeterModelEntityConverter : ModelEntityConverter<
  IMeter, MeterEntity>
{
  protected override MeterEntity ToEntity(
    IMeter model)
  {
    return model.ToEntity();
  }

  protected override IMeter ToModel(
    MeterEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeterModelEntityConverterExtensions
{
  public static MeterEntity ToEntity(this IMeter model)
  {
    if (model is AbbB2xMeterModel abbB2xMeterModel)
    {
      return abbB2xMeterModel.ToEntity();
    }

    if (model is SchneideriEM3xxxMeterModel schneideriEM3xxxMeterModel)
    {
      return schneideriEM3xxxMeterModel.ToEntity();
    }

    return new MeterEntity
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
      ConnectionPower_W = (float)model.ConnectionPower_W,
      Phases = model.Phases.Select(phase => phase.ToEntity()).ToList()
    };
  }

  public static IMeter ToModel(this MeterEntity entity)
  {
    if (entity is AbbB2xMeterEntity abbB2xMeterEntity)
    {
      return abbB2xMeterEntity.ToModel();
    }

    if (entity is SchneideriEM3xxxMeterEntity schneideriEM3xxxMeterEntity)
    {
      return schneideriEM3xxxMeterEntity.ToModel();
    }

    return new MeterModel
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
      MeasurementValidatorId = "",
      ConnectionPower_W = (decimal)entity.ConnectionPower_W,
      Phases = entity.Phases.Select(phase => phase.ToModel()).ToHashSet()
    };
  }
}
