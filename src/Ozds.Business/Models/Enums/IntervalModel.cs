using DataIntervalEntity = Ozds.Data.Entities.Enums.IntervalEntity;
using TimeIntervalEntity = Ozds.Time.Entities.IntervalEntity;

namespace Ozds.Business.Models.Enums;

public enum IntervalModel
{
  QuarterHour,
  Day,
  Month,
}

public static class IntervalModelExtensions
{
  public static IntervalModel ToModel(this DataIntervalEntity entity)
  {
    return entity switch
    {
      DataIntervalEntity.QuarterHour => IntervalModel.QuarterHour,
      DataIntervalEntity.Day => IntervalModel.Day,
      DataIntervalEntity.Month => IntervalModel.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null),
    };
  }

  public static DataIntervalEntity ToDataEntity(this IntervalModel model)
  {
    return model switch
    {
      IntervalModel.QuarterHour => DataIntervalEntity.QuarterHour,
      IntervalModel.Day => DataIntervalEntity.Day,
      IntervalModel.Month => DataIntervalEntity.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null),
    };
  }

  public static IntervalModel ToModel(this TimeIntervalEntity entity)
  {
    return entity switch
    {
      TimeIntervalEntity.QuarterHour => IntervalModel.QuarterHour,
      TimeIntervalEntity.Day => IntervalModel.Day,
      TimeIntervalEntity.Month => IntervalModel.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null),
    };
  }

  public static TimeIntervalEntity ToTimeEntity(this IntervalModel model)
  {
    return model switch
    {
      IntervalModel.QuarterHour => TimeIntervalEntity.QuarterHour,
      IntervalModel.Day => TimeIntervalEntity.Day,
      IntervalModel.Month => TimeIntervalEntity.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null),
    };
  }
}
