using Ozds.Business.Models.Enums;

namespace Ozds.Business.Queries.Abstractions;

public static class QueryConstants
{
  public const int StartingPage = 1;

  public const int DefaultPageCount = 50;

  public const int DefaultFinancialPageCount = 1000;

  public const int DefaultMeasurementPageCount = 1000;

  public static TimeSpan AggregateThreshold(
    IntervalModel interval,
    DateTimeOffset timestamp,
    int meterCount = 1,
    int pageCount = DefaultMeasurementPageCount
  )
  {
    var higherResolutionTimeSpan = interval.HigherResolutionTimeSpan(timestamp);
    return pageCount * higherResolutionTimeSpan / meterCount;
  }

  public static IntervalModel? AppropriateInterval(
    TimeSpan timeSpan,
    DateTimeOffset timestamp,
    int meterCount = 1,
    int pageCount = DefaultMeasurementPageCount
  )
  {
    var quarterHourThreshold = AggregateThreshold(
      IntervalModel.QuarterHour,
      timestamp,
      meterCount,
      pageCount);
    if (timeSpan < quarterHourThreshold)
    {
      return null;
    }

    var dayThreshold = AggregateThreshold(
      IntervalModel.Day,
      timestamp,
      meterCount,
      pageCount);
    if (timeSpan < dayThreshold)
    {
      return IntervalModel.QuarterHour;
    }

    var monthThreshold = AggregateThreshold(
      IntervalModel.Month,
      timestamp,
      meterCount,
      pageCount);
    if (timeSpan < monthThreshold)
    {
      return IntervalModel.Day;
    }

    return IntervalModel.Month;
  }
}
