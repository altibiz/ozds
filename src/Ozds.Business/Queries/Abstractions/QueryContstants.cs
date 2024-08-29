using Ozds.Business.Models.Enums;

namespace Ozds.Business.Queries.Abstractions;

public static class QueryConstants
{
  public const int StartingPage = 1;

  public const int DefaultPageCount = 50;
  public const int MeasurementPageCount = 10000;

  public static TimeSpan AggregateThreshold(
    IntervalModel interval,
    DateTimeOffset timestamp,
    int pageCount = MeasurementPageCount
  )
  {
    var higherResolutionTimeSpan = interval.HigherResolutionTimeSpan(timestamp);
    return pageCount * higherResolutionTimeSpan;
  }

  public static IntervalModel? AppropriateInterval(
    TimeSpan timeSpan,
    DateTimeOffset timestamp,
    int pageCount = MeasurementPageCount
  )
  {
    var quarterHourThreshold = AggregateThreshold(
      IntervalModel.QuarterHour,
      timestamp,
      pageCount);
    if (timeSpan < quarterHourThreshold)
    {
      return null;
    }

    var dayThreshold = AggregateThreshold(
      IntervalModel.Day,
      timestamp,
      pageCount);
    if (timeSpan < dayThreshold)
    {
      return IntervalModel.QuarterHour;
    }

    var monthThreshold = AggregateThreshold(
      IntervalModel.Month,
      timestamp,
      pageCount);
    if (timeSpan < monthThreshold)
    {
      return IntervalModel.Day;
    }

    return IntervalModel.Month;
  }
}
