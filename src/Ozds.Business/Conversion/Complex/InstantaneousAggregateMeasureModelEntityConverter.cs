using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class InstantaneousAggregateMeasureModelEntityConverter
  : ModelEntityConverter<
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
      Avg = (float)model.Avg,
      Min = (float)model.Min,
      Max = (float)model.Max,
      MinTimestamp = model.MinTimestamp,
      MaxTimestamp = model.MaxTimestamp
    };
  }

  public static InstantaneousAggregateMeasureModel ToModel(
    this InstantaneousAggregateMeasureEntity entity)
  {
    return new InstantaneousAggregateMeasureModel
    {
      Avg = (decimal)entity.Avg,
      Min = (decimal)entity.Min,
      Max = (decimal)entity.Max,
      MinTimestamp = entity.MinTimestamp,
      MaxTimestamp = entity.MaxTimestamp
    };
  }
}
