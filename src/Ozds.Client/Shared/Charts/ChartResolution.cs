using Ozds.Business.Time;

namespace Ozds.Client.Shared.Charts;

public enum ChartResolution
{
  Minute,
  Hour,
  Day,
  Week,
  Month,
  Year
}

public static class ChartResolutionExtensions
{
  public static int MaxMultiplier(this ChartResolution resolution)
  {
    return resolution switch
    {
      ChartResolution.Minute => 60,
      ChartResolution.Hour => 24,
      ChartResolution.Day => 30,
      ChartResolution.Week => 4,
      ChartResolution.Month => 12,
      ChartResolution.Year => 2,
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null)
    };
  }

  public static string ToTitle(this ChartResolution resolution, int multiplier)
  {
    if (multiplier == 1)
    {
      return resolution switch
      {
        ChartResolution.Minute => "Minute",
        ChartResolution.Hour => "Hour",
        ChartResolution.Day => "Day",
        ChartResolution.Week => "Week",
        ChartResolution.Month => "Month",
        ChartResolution.Year => "Year",
        _ => throw new ArgumentOutOfRangeException(
          nameof(resolution),
          resolution,
          null)
      };
    }

    return resolution switch
    {
      ChartResolution.Minute => "Minutes",
      ChartResolution.Hour => "Hours",
      ChartResolution.Day => "Days",
      ChartResolution.Week => "Weeks",
      ChartResolution.Month => "Months",
      ChartResolution.Year => "Years",
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null)
    };
  }

  public static TimeSpan ToTimeSpan(
    this ChartResolution resolution,
    int multiplier,
    DateTimeOffset timestamp)
  {
    return resolution switch
    {
      ChartResolution.Minute => TimeSpan.FromMinutes(1) * multiplier,
      ChartResolution.Hour => TimeSpan.FromHours(1) * multiplier,
      ChartResolution.Day => TimeSpan.FromDays(1) * multiplier,
      ChartResolution.Week => TimeSpan.FromDays(7) * multiplier,
      ChartResolution.Month => timestamp.GetMonthRange() switch
      {
        (DateTimeOffset start, DateTimeOffset end) => (end - start) * multiplier
      },
      ChartResolution.Year => TimeSpan.FromDays(365) * multiplier,
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null)
    };
  }
}
