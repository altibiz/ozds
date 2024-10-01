using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum DurationModel
{
  Second,
  Minute,
  Hour,
  Day,
  Week,
  Month,
  Year
}

public static class DurationModelExtensions
{
  public static TimeSpan ToTimeSpan(this DurationModel duration)
  {
    return duration switch
    {
      DurationModel.Second => TimeSpan.FromSeconds(1),
      DurationModel.Minute => TimeSpan.FromMinutes(1),
      DurationModel.Hour => TimeSpan.FromHours(1),
      DurationModel.Day => TimeSpan.FromDays(1),
      DurationModel.Week => TimeSpan.FromDays(7),
      DurationModel.Month => TimeSpan.FromDays(30),
      DurationModel.Year => TimeSpan.FromDays(365),
      _ => throw new NotImplementedException()
    };
  }

  public static string ToTitle(this DurationModel duration, bool plural = false)
  {
    if (plural)
    {
      return duration switch
      {
        DurationModel.Second => "seconds",
        DurationModel.Minute => "minutes",
        DurationModel.Hour => "hours",
        DurationModel.Day => "days",
        DurationModel.Week => "weeks",
        DurationModel.Month => "months",
        DurationModel.Year => "years",
        _ => throw new NotImplementedException()
      };
    }

    return duration switch
    {
      DurationModel.Second => "second",
      DurationModel.Minute => "minute",
      DurationModel.Hour => "hour",
      DurationModel.Day => "day",
      DurationModel.Week => "week",
      DurationModel.Month => "month",
      DurationModel.Year => "year",
      _ => throw new NotImplementedException()
    };
  }

  public static DurationModel ToModel(this DurationEntity entity)
  {
    return entity switch
    {
      DurationEntity.Second => DurationModel.Second,
      DurationEntity.Minute => DurationModel.Minute,
      DurationEntity.Hour => DurationModel.Hour,
      DurationEntity.Day => DurationModel.Day,
      DurationEntity.Week => DurationModel.Week,
      DurationEntity.Month => DurationModel.Month,
      DurationEntity.Year => DurationModel.Year,
      _ => throw new NotImplementedException()
    };
  }

  public static DurationEntity ToEntity(this DurationModel model)
  {
    return model switch
    {
      DurationModel.Second => DurationEntity.Second,
      DurationModel.Minute => DurationEntity.Minute,
      DurationModel.Hour => DurationEntity.Hour,
      DurationModel.Day => DurationEntity.Day,
      DurationModel.Week => DurationEntity.Week,
      DurationModel.Month => DurationEntity.Month,
      DurationModel.Year => DurationEntity.Year,
      _ => throw new NotImplementedException()
    };
  }
}
