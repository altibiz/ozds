using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using TimeTimeQueries = Ozds.Time.Queries.Abstractions.ITimeQueries;

namespace Ozds.Business.Queries;

public class TimeQueries(
  TimeTimeQueries timeTimeQueries
) : ISingletonQueries
{
  public TimeSpan GetOffset(DateTimeOffset forDate)
  {
    return timeTimeQueries.GetOffset(forDate);
  }

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetMonthRange(dateTimeOffset);
  }

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    int year,
    int month
  )
  {
    return timeTimeQueries.GetMonthRange(year, month);
  }

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetYearRange(dateTimeOffset);
  }

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    int year
  )
  {
    return timeTimeQueries.GetYearRange(year);
  }

  public DateTimeOffset GetStartOfQuarterHour(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfQuarterHour(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfMonth(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfLastMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfLastMonth(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfNextMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfNextMonth(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfDay(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfDay(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfYear(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfYear(dateTimeOffset);
  }

  public IEnumerable<DateTimeOffset> GetThisYearMonthStarts(
    DateTimeOffset dateTimeOffset)
  {
    return timeTimeQueries.GetThisYearMonthStarts(dateTimeOffset);
  }

  public DateTimeOffset GetStartOfMonthLastYear(
    DateTimeOffset dateTimeOffset
  )
  {
    return timeTimeQueries.GetStartOfMonthLastYear(dateTimeOffset);
  }

  public TimeSpan ResolutionTimeSpan(
    ResolutionModel resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    return timeTimeQueries.ResolutionTimeSpan(
      resolution.ToTimeEntity(),
      timestamp,
      multiplier);
  }

  public TimeSpan IntervalTimeSpan(
    IntervalModel model,
    DateTimeOffset timestamp
  )
  {
    return timeTimeQueries.IntervalTimeSpan(
      model.ToTimeEntity(),
      timestamp);
  }

  public TimeSpan HigherResolutionIntervalTimeSpan(
    IntervalModel model,
    DateTimeOffset timestamp
  )
  {
    return timeTimeQueries.HigherResolutionIntervalTimeSpan(
      model.ToTimeEntity(),
      timestamp);
  }

  public TimeSpan AggregateThreshold(
    IntervalModel interval,
    DateTimeOffset timestamp,
    int meterCount = 1,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    return timeTimeQueries.AggregateThreshold(
      interval.ToTimeEntity(),
      timestamp,
      meterCount,
      pageCount);
  }

  public IntervalModel? AppropriateInterval(
    TimeSpan timeSpan,
    DateTimeOffset timestamp,
    int meterCount = 1,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    return timeTimeQueries
      .AppropriateInterval(timeSpan, timestamp, meterCount, pageCount)
      ?.ToModel();
  }

  public TimeSpan DurationTimeSpan(
    DurationModel model,
    uint multiplier = 1
  )
  {
    return timeTimeQueries.DurationTimeSpan(
      model.ToTimeEntity(),
      multiplier);
  }

  public TimeSpan DurationTimeSpan(
    DurationModel model,
    DateTimeOffset timestamp,
    uint multiplier = 1
  )
  {
    return timeTimeQueries.DurationTimeSpan(
      model.ToTimeEntity(),
      timestamp,
      multiplier);
  }

  public TimeSpan PeriodTimeSpan(
    PeriodModel model
  )
  {
    return timeTimeQueries.DurationTimeSpan(
      model.Duration.ToTimeEntity(),
      model.Multiplier);
  }

  public TimeSpan PeriodTimeSpan(
    PeriodModel model,
    DateTimeOffset timestamp
  )
  {
    return timeTimeQueries.DurationTimeSpan(
      model.Duration.ToTimeEntity(),
      timestamp,
      model.Multiplier);
  }
}
