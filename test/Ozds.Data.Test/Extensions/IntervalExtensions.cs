using DataIntervalEntity = Ozds.Data.Entities.Enums.IntervalEntity;
using TimeIntervalEntity = Ozds.Time.Entities.IntervalEntity;

namespace Ozds.Data.Test.Extensions;

public static class IntervalExtensions
{
  public static TimeIntervalEntity ToTimeEntity(
    this DataIntervalEntity interval
  )
  {
    return interval switch
    {
      DataIntervalEntity.QuarterHour => TimeIntervalEntity.QuarterHour,
      DataIntervalEntity.Day => TimeIntervalEntity.Day,
      DataIntervalEntity.Month => TimeIntervalEntity.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(interval))
    };
  }
}
