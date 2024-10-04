using Ozds.Business.Time;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum IntervalModel
{
  QuarterHour,
  Day,
  Month
}

public static class IntervalModelExtensions
{
  public static IntervalModel ToModel(this IntervalEntity entity)
  {
    return entity switch
    {
      IntervalEntity.QuarterHour => IntervalModel.QuarterHour,
      IntervalEntity.Day => IntervalModel.Day,
      IntervalEntity.Month => IntervalModel.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }

  public static IntervalEntity ToEntity(this IntervalModel model)
  {
    return model switch
    {
      IntervalModel.QuarterHour => IntervalEntity.QuarterHour,
      IntervalModel.Day => IntervalEntity.Day,
      IntervalModel.Month => IntervalEntity.Month,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static TimeSpan ToTimeSpan(
    this IntervalModel model,
    DateTimeOffset timestamp)
  {
    return model switch
    {
      IntervalModel.QuarterHour => TimeSpan.FromMinutes(15),
      IntervalModel.Day => TimeSpan.FromDays(1),
      IntervalModel.Month => timestamp.GetMonthRange() switch
      {
        (DateTimeOffset start, DateTimeOffset end) => end - start
      },
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static TimeSpan HigherResolutionTimeSpan(
    this IntervalModel model,
#pragma warning disable IDE0060 // Remove unused parameter
    DateTimeOffset timestamp)
#pragma warning restore IDE0060 // Remove unused parameter
  {
    return model switch
    {
      IntervalModel.QuarterHour => TimeSpan.FromSeconds(1),
      IntervalModel.Day => TimeSpan.FromMinutes(15),
      IntervalModel.Month => TimeSpan.FromDays(1),
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
