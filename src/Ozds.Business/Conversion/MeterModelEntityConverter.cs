using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class
  MeterModelEntityConverter : ModelEntityConverter<MeterModel,
  MeterEntity>
{
  protected override MeterEntity ToEntity(MeterModel model)
  {
    return model.ToEntity();
  }

  protected override MeterModel ToModel(MeterEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MeterModelEntityConverterExtensions
{
  public static MeterEntity ToEntity(this MeterModel model)
  {
    if (model is AbbB2xMeterModel abbB2XMeterModel)
    {
      return abbB2XMeterModel.ToEntity();
    }
    else if (model is SchneideriEM3xxxMeterModel schneideriEM3xxxMeterModel)
    {
      return schneideriEM3xxxMeterModel.ToEntity();
    }
    throw new InvalidOperationException("");
  }

  public static MeterModel ToModel(this MeterEntity entity)
  {
    if (entity is AbbB2xMeterEntity abbB2XMeterEntity)
    {
      return abbB2XMeterEntity.ToModel();
    }
    else if (entity is SchneideriEM3xxxMeterEntity schneideriEM3xxxMeterEntity)
    {
      return schneideriEM3xxxMeterEntity.ToModel();
    }
    throw new InvalidOperationException("");
  }
}
