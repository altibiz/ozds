using Ozds.Time.Entities;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Time.Queries.Implementations;

// NOTE: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones

public class TimeQueries : ITimeQueries
{
  private static readonly TimeZoneInfo CroatianTimeZoneValue =
    TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb");

  private static readonly TimeZoneInfo UtcTimeZoneValue =
    TimeZoneInfo.FindSystemTimeZoneById("Etc/UTC");

  public TimeZoneInfo DefaultTimeZone { get; } =
    UtcTimeZoneValue;

  public TimeZoneInfo CroatianTimeZone { get; } =
    CroatianTimeZoneValue;

  public TimeZoneInfo UtcTimeZone { get; } =
    UtcTimeZoneValue;

  public TimeZoneInfo? IdToTimeZone(string timeZone)
  {
    try
    {
      return TimeZoneInfo.FindSystemTimeZoneById(timeZone);
    }
    catch (Exception)
    {
      return null;
    }
  }

  public string TimeZoneToId(TimeZoneInfo timeZone)
  {
    return timeZone.Id;
  }

  public string TimeZoneToName(TimeZoneInfo timeZone)
  {
    return timeZone.DisplayName;
  }

  public TimeSpan GetCroatianOffset(DateTimeOffset forDate)
  {
    return CroatianTimeZone.GetUtcOffset(forDate);
  }

  public TimeSpan GetUtcOffset(DateTimeOffset forDate)
  {
    return UtcTimeZone.GetUtcOffset(forDate);
  }

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfMonth = new DateTime(
      localDateTime.Year, localDateTime.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfMonth =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfMonth,
        CroatianTimeZone);

    var localStartOfNextMonth = localStartOfMonth.AddMonths(1);

    var utcStartOfNextMonth =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfNextMonth,
        CroatianTimeZone);

    return (
      new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero),
      new DateTimeOffset(utcStartOfNextMonth, TimeSpan.Zero)
    );
  }

  public (DateTimeOffset, DateTimeOffset) GetMonthRange(
    int year,
    int month
  )
  {
    var startOfMonth = new DateTimeOffset(
      year,
      month,
      15, // NOTE: avoiding possible time zone issues
      0,
      0,
      0,
      CroatianTimeZone.BaseUtcOffset
    );

    return GetMonthRange(startOfMonth);
  }

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfYear = new DateTime(
      localDateTime.Year, 1, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfYear =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfYear,
        CroatianTimeZone);

    var localStartOfNextYear = localStartOfYear.AddYears(1);

    var utcStartOfNextYear =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfNextYear,
        CroatianTimeZone);

    return (
      new DateTimeOffset(utcStartOfYear, TimeSpan.Zero),
      new DateTimeOffset(utcStartOfNextYear, TimeSpan.Zero)
    );
  }

  public (DateTimeOffset, DateTimeOffset) GetYearRange(
    int year
  )
  {
    var startOfMonth = new DateTimeOffset(
      year,
      1,
      15, // NOTE: avoiding possible time zone issues
      0,
      0,
      0,
      CroatianTimeZone.BaseUtcOffset
    );

    return GetYearRange(startOfMonth);
  }

  // NOTE: one in UTC because clock rewind on 27.10. makes
  // conversion back to UTC from Croatian ambiguous
  public DateTimeOffset GetStartOfQuarterHour(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      TimeZoneInfo.Utc
    );

    var quarterHour = localDateTime.Minute / 15 * 15;

    var localStartOfQuarterHour = new DateTime(
      localDateTime.Year,
      localDateTime.Month,
      localDateTime.Day,
      localDateTime.Hour,
      quarterHour,
      0,
      DateTimeKind.Unspecified);

    var utcStartOfQuarterHour = TimeZoneInfo.ConvertTimeToUtc(
      localStartOfQuarterHour, TimeZoneInfo.Utc);

    return new DateTimeOffset(utcStartOfQuarterHour, TimeSpan.Zero);
  }

  public DateTimeOffset GetStartOfMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfMonth = new DateTime(
      localDateTime.Year, localDateTime.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfMonth =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfMonth,
        CroatianTimeZone);

    return new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero);
  }

  public DateTimeOffset GetStartOfLastMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var lastMonthLocalDateTime = localDateTime.AddMonths(-1);

    var localStartOfLastMonth = new DateTime(
      lastMonthLocalDateTime.Year, lastMonthLocalDateTime.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfLastMonth =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfLastMonth,
        CroatianTimeZone);

    return new DateTimeOffset(utcStartOfLastMonth, TimeSpan.Zero);
  }

  public DateTimeOffset GetStartOfNextMonth(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var nextMonthLocalDateTime = localDateTime.AddMonths(1);

    var localStartOfNextMonth = new DateTime(
      nextMonthLocalDateTime.Year, nextMonthLocalDateTime.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfNextMonth =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfNextMonth,
        CroatianTimeZone);

    return new DateTimeOffset(utcStartOfNextMonth, TimeSpan.Zero);
  }

  public DateTimeOffset GetStartOfDay(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfDay = new DateTime(
      localDateTime.Year, localDateTime.Month, localDateTime.Day, 0, 0, 0,
      DateTimeKind.Unspecified);

    var utcStartOfDay =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfDay,
        CroatianTimeZone);

    return new DateTimeOffset(utcStartOfDay, TimeSpan.Zero);
  }

  public DateTimeOffset GetStartOfYear(
    DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfYear = new DateTime(
      localDateTime.Year, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

    var utcStartOfYear =
      TimeZoneInfo.ConvertTimeToUtc(
        localStartOfYear,
        CroatianTimeZone);
    return new DateTimeOffset(utcStartOfYear, TimeSpan.Zero);
  }

  public IEnumerable<DateTimeOffset> GetThisYearMonthStarts(
    DateTimeOffset dateTimeOffset)
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var year = localDateTime.Year;

    return Enumerable.Range(1, 12).Select(month =>
    {
      var localStartOfMonth = new DateTime(
        year, month, 1, 0, 0, 0, DateTimeKind.Unspecified);

      var utcStartOfMonth =
        TimeZoneInfo.ConvertTimeToUtc(
          localStartOfMonth,
          CroatianTimeZone);

      return new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero);
    });
  }

  public DateTimeOffset GetStartOfMonthLastYear(
    DateTimeOffset dateTimeOffset
  )
  {
    return GetStartOfMonth(dateTimeOffset.AddYears(-1));
  }

  public TimeSpan ResolutionTimeSpan(
    ResolutionEntity resolution,
    DateTimeOffset timestamp,
    int multiplier
  )
  {
    return resolution switch
    {
      ResolutionEntity.Minute => TimeSpan.FromMinutes(1) * multiplier,
      ResolutionEntity.Hour => TimeSpan.FromHours(1) * multiplier,
      ResolutionEntity.Day => TimeSpan.FromDays(1) * multiplier,
      ResolutionEntity.Week => TimeSpan.FromDays(7) * multiplier,
      ResolutionEntity.Month => GetMonthRange(timestamp) switch
      {
        (DateTimeOffset start, DateTimeOffset end) => (end - start) * multiplier
      },
      ResolutionEntity.Year => GetYearRange(timestamp) switch
      {
        (DateTimeOffset start, DateTimeOffset end) => (end - start) * multiplier
      },
      _ => throw new ArgumentOutOfRangeException(
        nameof(resolution),
        resolution,
        null)
    };
  }

  public TimeSpan IntervalTimeSpan(
    IntervalEntity model,
    DateTimeOffset timestamp
  )
  {
    return model switch
    {
      IntervalEntity.QuarterHour => TimeSpan.FromMinutes(15),
      IntervalEntity.Day => TimeSpan.FromDays(1),
      IntervalEntity.Month => GetMonthRange(timestamp) switch
      {
        (DateTimeOffset start, DateTimeOffset end) => end - start
      },
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public TimeSpan HigherResolutionIntervalTimeSpan(
    IntervalEntity model,
    DateTimeOffset timestamp
  )
  {
    return model switch
    {
      // We need to support up to 1 second
      // because that's the optimal speed of getting measurements
      IntervalEntity.QuarterHour => TimeSpan.FromSeconds(1),
      IntervalEntity.Day => TimeSpan.FromMinutes(15),
      IntervalEntity.Month => TimeSpan.FromDays(1),
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public TimeSpan AggregateThreshold(
    IntervalEntity interval,
    DateTimeOffset timestamp,
    int sourceCount,
    int pageCount
  )
  {
    var higherResolutionTimeSpan =
      HigherResolutionIntervalTimeSpan(interval, timestamp);
    return pageCount * higherResolutionTimeSpan / sourceCount;
  }

  public IntervalEntity? AppropriateInterval(
    TimeSpan timeSpan,
    DateTimeOffset timestamp,
    int sourceCount,
    int pageCount
  )
  {
    var quarterHourThreshold = AggregateThreshold(
      IntervalEntity.QuarterHour,
      timestamp,
      sourceCount,
      pageCount);
    if (timeSpan < quarterHourThreshold)
    {
      return null;
    }

    var dayThreshold = AggregateThreshold(
      IntervalEntity.Day,
      timestamp,
      sourceCount,
      pageCount);
    if (timeSpan < dayThreshold)
    {
      return IntervalEntity.QuarterHour;
    }

    var monthThreshold = AggregateThreshold(
      IntervalEntity.Month,
      timestamp,
      sourceCount,
      pageCount);
    if (timeSpan < monthThreshold)
    {
      return IntervalEntity.Day;
    }

    return IntervalEntity.Month;
  }

  public TimeSpan DurationTimeSpan(
    DurationEntity model,
    DateTimeOffset timestamp,
    uint multiplier = 1
  )
  {
    return model switch
    {
      DurationEntity.Second => TimeSpan.FromSeconds(multiplier),
      DurationEntity.Minute => TimeSpan.FromMinutes(multiplier),
      DurationEntity.Hour => TimeSpan.FromHours(multiplier),
      DurationEntity.Day => TimeSpan.FromDays(multiplier),
      DurationEntity.Week => TimeSpan.FromDays(7 * multiplier),
      DurationEntity.Month => GetMonthRange(timestamp) switch
      {
        (DateTimeOffset start, DateTimeOffset end) => end - start
      },
      DurationEntity.Year => GetYearRange(timestamp) switch
      {
        (DateTimeOffset start, DateTimeOffset end) => end - start
      },
      _ => throw new ArgumentOutOfRangeException(
        nameof(model),
        model,
        null)
    };
  }

  public TimeSpan DurationTimeSpan(
    DurationEntity model,
    uint multiplier = 1
  )
  {
    return model switch
    {
      DurationEntity.Second => TimeSpan.FromSeconds(multiplier),
      DurationEntity.Minute => TimeSpan.FromMinutes(multiplier),
      DurationEntity.Hour => TimeSpan.FromHours(multiplier),
      DurationEntity.Day => TimeSpan.FromDays(multiplier),
      DurationEntity.Week => TimeSpan.FromDays(7 * multiplier),
      DurationEntity.Month => TimeSpan.FromDays(30 * multiplier),
      DurationEntity.Year => TimeSpan.FromDays(365 * multiplier),
      _ => throw new ArgumentOutOfRangeException(
        nameof(model),
        model,
        null)
    };
  }
}
