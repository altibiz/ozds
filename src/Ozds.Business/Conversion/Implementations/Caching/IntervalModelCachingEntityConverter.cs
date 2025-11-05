using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class IntervalModelCachingEntityConverter
  : ConcreteModelCachingEntityConverter<IntervalModel, IntervalEntity>
{
  public override IntervalEntity ToEntity(IntervalModel model)
  {
    return model switch
    {
      IntervalModel.QuarterHour => IntervalEntity.QuarterHour,
      IntervalModel.Day => IntervalEntity.Day,
      IntervalModel.Month => IntervalEntity.Month,
      _ => throw new NotImplementedException()
    };
  }

  public override IntervalModel ToModel(IntervalEntity entity)
  {
    return entity switch
    {
      IntervalEntity.QuarterHour => IntervalModel.QuarterHour,
      IntervalEntity.Day => IntervalModel.Day,
      IntervalEntity.Month => IntervalModel.Month,
      _ => throw new NotImplementedException()
    };
  }
}
