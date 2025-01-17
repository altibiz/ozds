using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class InstantaneousAggregateMeasureModelEntityConverter
  : ConcreteModelEntityConverter<
    InstantaneousAggregateMeasureModel,
    InstantaneousAggregateMeasureEntity>
{
  protected override InstantaneousAggregateMeasureEntity ToEntity(
    InstantaneousAggregateMeasureModel model)
  {
    return model.ToEntity();
  }

  protected override InstantaneousAggregateMeasureModel ToModel(
    InstantaneousAggregateMeasureEntity entity)
  {
    return entity.ToModel();
  }
}

public static class InstantaneousAggregateMeasureModelEntityConverterExtensions
{
  public static InstantaneousAggregateMeasureEntity ToEntity(
    this InstantaneousAggregateMeasureModel model)
  {
    return new InstantaneousAggregateMeasureEntity
    {
      Avg = model.Avg.ToFloat(),
      Min = model.Min.ToFloat(),
      Max = model.Max.ToFloat(),
      MinTimestamp = model.MinTimestamp,
      MaxTimestamp = model.MaxTimestamp
    };
  }

  public static InstantaneousAggregateMeasureModel ToModel(
    this InstantaneousAggregateMeasureEntity entity)
  {
    return new InstantaneousAggregateMeasureModel
    {
      Avg = entity.Avg.ToDecimal(),
      Min = entity.Min.ToDecimal(),
      Max = entity.Max.ToDecimal(),
      MinTimestamp = entity.MinTimestamp,
      MaxTimestamp = entity.MaxTimestamp
    };
  }
}
