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

    if (model is MeterModel meterModel)
    {
      return new MeterEntity
      {
        Id = meterModel.Id,
        Title = meterModel.Title,
        CreatedOn = meterModel.CreatedOn,
        CreatedById = meterModel.CreatedById,
        LastUpdatedOn = meterModel.LastUpdatedOn,
        LastUpdatedById = meterModel.LastUpdatedById,
        IsDeleted = meterModel.IsDeleted,
        DeletedOn = meterModel.DeletedOn,
        DeletedById = meterModel.DeletedById,
        MessengerId = meterModel.MessengerId,
        ConnectionPower_W = meterModel.ConnectionPower_W,
        Phases = meterModel.Phases.Select(phase => phase.ToEntity()).ToList()
      };
    }

    throw new NotSupportedException(
      $"MeterModel type {model.GetType().Name} is not supported."
    );
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

    if (entity is MeterEntity meterEntity)
    {
      return new MeterModel
      {
        Id = meterEntity.Id,
        Title = meterEntity.Title,
        CreatedOn = meterEntity.CreatedOn,
        CreatedById = meterEntity.CreatedById,
        LastUpdatedOn = meterEntity.LastUpdatedOn,
        LastUpdatedById = meterEntity.LastUpdatedById,
        IsDeleted = meterEntity.IsDeleted,
        DeletedOn = meterEntity.DeletedOn,
        DeletedById = meterEntity.DeletedById,
        MessengerId = meterEntity.MessengerId,
        MeasurementValidatorId = "",
        ConnectionPower_W = meterEntity.ConnectionPower_W,
        Phases = meterEntity.Phases.Select(phase => phase.ToModel()).ToHashSet()
      };
    }

    throw new NotSupportedException(
      $"MeterEntity type {entity.GetType().Name} is not supported."
    );
  }
}
