using TimeResolutionEntity = Ozds.Time.Entities.ResolutionEntity;

namespace Ozds.Business.Models.Enums;

public enum ResolutionModel
{
  Minute,
  Hour,
  Day,
  Week,
  Month,
  Year,
}

public static class ChartResolutionExtensions
{
  public static int MaxMultiplier(this ResolutionModel resolution)
  {
    return resolution switch
    {
      ResolutionModel.Minute => 60,
      ResolutionModel.Hour => 24,
      ResolutionModel.Day => 30,
      ResolutionModel.Week => 4,
      ResolutionModel.Month => 12,
      ResolutionModel.Year => 2,
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null
      ),
    };
  }

  public static string ToTitle(this ResolutionModel resolution, int multiplier)
  {
    if (multiplier == 1)
    {
      return resolution switch
      {
        ResolutionModel.Minute => "minute",
        ResolutionModel.Hour => "hour",
        ResolutionModel.Day => "day",
        ResolutionModel.Week => "week",
        ResolutionModel.Month => "month",
        ResolutionModel.Year => "year",
        _ => throw new ArgumentOutOfRangeException(
          nameof(resolution),
          resolution,
          null
        ),
      };
    }

    return resolution switch
    {
      ResolutionModel.Minute => "minutes",
      ResolutionModel.Hour => "hours",
      ResolutionModel.Day => "days",
      ResolutionModel.Week => "weeks",
      ResolutionModel.Month => "months",
      ResolutionModel.Year => "years",
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null
      ),
    };
  }

  public static ResolutionModel ToModel(this TimeResolutionEntity entity)
  {
    return entity switch
    {
      TimeResolutionEntity.Minute => ResolutionModel.Minute,
      TimeResolutionEntity.Hour => ResolutionModel.Hour,
      TimeResolutionEntity.Day => ResolutionModel.Day,
      TimeResolutionEntity.Week => ResolutionModel.Week,
      TimeResolutionEntity.Month => ResolutionModel.Month,
      TimeResolutionEntity.Year => ResolutionModel.Year,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null),
    };
  }

  public static TimeResolutionEntity ToTimeEntity(this ResolutionModel model)
  {
    return model switch
    {
      ResolutionModel.Minute => TimeResolutionEntity.Minute,
      ResolutionModel.Hour => TimeResolutionEntity.Hour,
      ResolutionModel.Day => TimeResolutionEntity.Day,
      ResolutionModel.Week => TimeResolutionEntity.Week,
      ResolutionModel.Month => TimeResolutionEntity.Month,
      ResolutionModel.Year => TimeResolutionEntity.Year,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null),
    };
  }
}
