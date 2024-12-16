using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class CumulativeAggregateMeasureModelEntityConverter
  : ModelEntityConverter<
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
      Min = (float)model.Min,
      Max = (float)model.Max,
    };
  }

  public static CumulativeAggregateMeasureModel ToModel(
    this CumulativeAggregateMeasureEntity entity)
  {
    return new CumulativeAggregateMeasureModel
    {
      Min = (decimal)entity.Min,
      Max = (decimal)entity.Max,
    };
  }
}
