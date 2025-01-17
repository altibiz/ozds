using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class CumulativeAggregateMeasureModelEntityConverter
  : ConcreteModelEntityConverter<
    CumulativeAggregateMeasureModel,
    CumulativeAggregateMeasureEntity>
{
  protected override CumulativeAggregateMeasureEntity ToEntity(
    CumulativeAggregateMeasureModel model)
  {
    return model.ToEntity();
  }

  protected override CumulativeAggregateMeasureModel ToModel(
    CumulativeAggregateMeasureEntity entity)
  {
    return entity.ToModel();
  }
}

public static class CumulativeAggregateMeasureModelEntityConverterExtensions
{
  public static CumulativeAggregateMeasureEntity ToEntity(
    this CumulativeAggregateMeasureModel model)
  {
    return new CumulativeAggregateMeasureEntity
    {
      Min = model.Min.ToFloat(),
      Max = model.Max.ToFloat(),
    };
  }

  public static CumulativeAggregateMeasureModel ToModel(
    this CumulativeAggregateMeasureEntity entity)
  {
    return new CumulativeAggregateMeasureModel
    {
      Min = entity.Min.ToDecimal(),
      Max = entity.Max.ToDecimal(),
    };
  }
}
