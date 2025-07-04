using DataDurationEntity = Ozds.Data.Entities.Enums.DurationEntity;
using TimeDurationEntity = Ozds.Time.Entities.DurationEntity;

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
  public static DurationModel ToModel(this DataDurationEntity entity)
  {
    return entity switch
    {
      DataDurationEntity.Second => DurationModel.Second,
      DataDurationEntity.Minute => DurationModel.Minute,
      DataDurationEntity.Hour => DurationModel.Hour,
      DataDurationEntity.Day => DurationModel.Day,
      DataDurationEntity.Week => DurationModel.Week,
      DataDurationEntity.Month => DurationModel.Month,
      DataDurationEntity.Year => DurationModel.Year,
      _ => throw new NotImplementedException()
    };
  }

  public static DataDurationEntity ToDataEntity(this DurationModel model)
  {
    return model switch
    {
      DurationModel.Second => DataDurationEntity.Second,
      DurationModel.Minute => DataDurationEntity.Minute,
      DurationModel.Hour => DataDurationEntity.Hour,
      DurationModel.Day => DataDurationEntity.Day,
      DurationModel.Week => DataDurationEntity.Week,
      DurationModel.Month => DataDurationEntity.Month,
      DurationModel.Year => DataDurationEntity.Year,
      _ => throw new NotImplementedException()
    };
  }

  public static DurationModel ToModel(this TimeDurationEntity entity)
  {
    return entity switch
    {
      TimeDurationEntity.Second => DurationModel.Second,
      TimeDurationEntity.Minute => DurationModel.Minute,
      TimeDurationEntity.Hour => DurationModel.Hour,
      TimeDurationEntity.Day => DurationModel.Day,
      TimeDurationEntity.Week => DurationModel.Week,
      TimeDurationEntity.Month => DurationModel.Month,
      TimeDurationEntity.Year => DurationModel.Year,
      _ => throw new NotImplementedException()
    };
  }

  public static TimeDurationEntity ToTimeEntity(this DurationModel model)
  {
    return model switch
    {
      DurationModel.Second => TimeDurationEntity.Second,
      DurationModel.Minute => TimeDurationEntity.Minute,
      DurationModel.Hour => TimeDurationEntity.Hour,
      DurationModel.Day => TimeDurationEntity.Day,
      DurationModel.Week => TimeDurationEntity.Week,
      DurationModel.Month => TimeDurationEntity.Month,
      DurationModel.Year => TimeDurationEntity.Year,
      _ => throw new NotImplementedException()
    };
  }
}
