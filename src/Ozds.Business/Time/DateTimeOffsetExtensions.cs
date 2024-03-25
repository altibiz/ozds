namespace Mess.Prelude.Extensions.Timestamps;

public static class DateTimeOffsetExtensions
{
  // NOTE: Croatian UTC offset (https://en.wikipedia.org/wiki/List_of_UTC_offsets)
  public static readonly TimeSpan DefaultOffset = TimeSpan.FromHours(1);

  public static (DateTimeOffset, DateTimeOffset) GetMonthRange(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    var monthStart = new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      1,
      0,
      0,
      0,
      dateTimeOffset.Offset
    );
    var nextMonthStart = monthStart.AddMonths(1);
    return (monthStart.ToUniversalTime(), nextMonthStart.ToUniversalTime());
  }

  public static DateTimeOffset GetStartOfQuarterHour(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    var quarterHour = dateTimeOffset.Minute / 15;
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      dateTimeOffset.Day,
      dateTimeOffset.Hour,
      quarterHour * 15,
      0,
      dateTimeOffset.Offset
    ).ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      1,
      0,
      0,
      0,
      dateTimeOffset.Offset
    ).ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfLastMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      1,
      0,
      0,
      0,
      dateTimeOffset.Offset
    ).AddMonths(-1).ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfNextMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      1,
      0,
      0,
      0,
      dateTimeOffset.Offset
    ).AddMonths(1).ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfDay(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      dateTimeOffset.Day,
      0,
      0,
      0,
      dateTimeOffset.Offset
    ).ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfYear(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return new DateTimeOffset(
      dateTimeOffset.Year,
      1,
      1,
      0,
      0,
      0,
      dateTimeOffset.Offset
    ).ToUniversalTime();
  }

  public static IEnumerable<DateTimeOffset> GetThisYearMonthStarts(
    this DateTimeOffset dateTimeOffset
  )
  {
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return Enumerable
      .Range(1, 12).Select(month =>
        new DateTimeOffset(
            dateTimeOffset.Year,
            month,
            1,
            0,
            0,
            0,
            dateTimeOffset.Offset
          )
          .ToUniversalTime()
      );
  }
}
