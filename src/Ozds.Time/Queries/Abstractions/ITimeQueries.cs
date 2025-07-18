using Ozds.Time.Entities;

namespace Ozds.Time.Queries.Abstractions;

public interface ITimeQueries : IQueries
{
  public TimeZoneInfo CroatianTimeZone { get; }

  public TimeZoneInfo UtcTimeZone { get; }

  public TimeSpan GetCroatianOffset(DateTimeOffset forDate);

  public TimeSpan GetUtcOffset(DateTimeOffset forDate);

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    DateTimeOffset dateTimeOffset
  );

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    int year,
    int month
  );

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    DateTimeOffset dateTimeOffset
  );

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    int year
  );

  public DateTimeOffset GetStartOfQuarterHour(
    DateTimeOffset dateTimeOffset
  );

  public DateTimeOffset GetStartOfMonth(
    DateTimeOffset dateTimeOffset
  );

  public DateTimeOffset GetStartOfLastMonth(
    DateTimeOffset dateTimeOffset
  );

  public DateTimeOffset GetStartOfNextMonth(
    DateTimeOffset dateTimeOffset
  );

  public DateTimeOffset GetStartOfDay(
    DateTimeOffset dateTimeOffset
  );

  public DateTimeOffset GetStartOfYear(
    DateTimeOffset dateTimeOffset
  );

  public IEnumerable<DateTimeOffset> GetThisYearMonthStarts(
    DateTimeOffset dateTimeOffset);

  public DateTimeOffset GetStartOfMonthLastYear(
    DateTimeOffset dateTimeOffset
  );

  public TimeSpan ResolutionTimeSpan(
    ResolutionEntity resolution,
    DateTimeOffset timestamp,
    int multiplier
  );

  public TimeSpan IntervalTimeSpan(
    IntervalEntity model,
    DateTimeOffset timestamp
  );

  public TimeSpan HigherResolutionIntervalTimeSpan(
    IntervalEntity model,
    DateTimeOffset timestamp
  );

  public TimeSpan AggregateThreshold(
    IntervalEntity interval,
    DateTimeOffset timestamp,
    int sourceCount,
    int pageCount
  );

  public IntervalEntity? AppropriateInterval(
    TimeSpan timeSpan,
    DateTimeOffset timestamp,
    int sourceCount,
    int pageCount
  );

  public TimeSpan DurationTimeSpan(
    DurationEntity model,
    uint multiplier = 1
  );

  public TimeSpan DurationTimeSpan(
    DurationEntity model,
    DateTimeOffset timestamp,
    uint multiplier = 1
  );
}
