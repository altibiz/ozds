using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class MeterModelEntityConverter : ModelEntityConverter<
  MeterModel, MeterEntity>
{
  protected override MeterEntity ToEntity(
    MeterModel model)
  {
    return model.ToEntity();
  }

  protected override MeterModel ToModel(
    MeterEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeterModelEntityConverterExtensions
{
  public static MeterEntity ToEntity(this MeterModel model)
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

    throw new NotSupportedException(
      $"MeterEntity type {entity.GetType().Name} is not supported."
    );
  }
}
