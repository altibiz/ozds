using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeterModelEntityConverter : ConcreteModelEntityConverter<
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

    throw new NotSupportedException(
      $"MeterModel type {model.GetType().Name} is not supported."
    );
  }

  public static MeterModel ToModel(this MeterEntity entity)
  {
    if (entity is AbbB2xMeterEntity abbB2xMeterEntity)
    {
      return abbB2xMeterEntity.ToModel();
    }

    if (entity is SchneideriEM3xxxMeterEntity schneideriEM3xxxMeterEntity)
    {
      return schneideriEM3xxxMeterEntity.ToModel();
    }

    // NOTE: for archived properties
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
      ConnectionPower_W = (decimal)entity.ConnectionPower_W,
      Phases = entity.Phases.Select(x => x.ToModel()).ToHashSet(),
      MessengerId = entity.MessengerId,
      Kind = entity.Kind
    };

    throw new NotSupportedException(
      $"MeterEntity type {entity.GetType().Name} is not supported."
    );
  }
}
