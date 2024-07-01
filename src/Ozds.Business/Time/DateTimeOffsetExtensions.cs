namespace Ozds.Business.Time;

public static class DateTimeOffsetExtensions
{
  // NOTE: Croatian UTC offset (https://en.wikipedia.org/wiki/List_of_UTC_offsets)

  public static TimeSpan DefaultOffset => TimeZoneInfo
    .FindSystemTimeZoneById("Europe/Zagreb").GetUtcOffset(DateTime.UtcNow);

  private static TimeSpan GetOffset(DateTimeOffset forDate)
  {
    return TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb")
      .GetUtcOffset(forDate);
  }

  public static (DateTimeOffset, DateTimeOffset) GetMonthRange(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnlyStart = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month,
      1, 0, 0, 0, DateTimeKind.Unspecified);
    var offsetStart = GetOffset(dateOnlyStart);

    var nextMonth = dateTimeOffset.AddMonths(1);
    var dateOnlyEnd = new DateTime(nextMonth.Year, nextMonth.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);
    var offsetEnd = GetOffset(dateOnlyEnd);

    var start = new DateTimeOffset(dateOnlyStart, offsetStart);
    var end = new DateTimeOffset(dateOnlyEnd, offsetEnd);
    return (start.ToUniversalTime(), end.ToUniversalTime());
  }

  public static DateTimeOffset GetStartOfQuarterHour(
    this DateTimeOffset dateTimeOffset
  )
  {
    var quarterHour = dateTimeOffset.Minute / 15;

    dateTimeOffset = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset));
    return new DateTimeOffset(
      dateTimeOffset.Year,
      dateTimeOffset.Month,
      dateTimeOffset.Day,
      dateTimeOffset.Hour,
      quarterHour * 15,
      0,
      dateTimeOffset.Offset
    );
  }

  public static DateTimeOffset GetStartOfMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0,
      0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfMonth = new DateTimeOffset(dateOnly, offset);
    return startOfMonth.ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfMonthNoTransformation(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0,
      0, 0, DateTimeKind.Unspecified);

    var startOfMonth = new DateTimeOffset(dateOnly, TimeSpan.Zero);
    return startOfMonth.ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfLastMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    var lastMonth = dateTimeOffset.AddMonths(-1);
    var dateOnly = new DateTime(lastMonth.Year, lastMonth.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfLastMonth = new DateTimeOffset(dateOnly, offset);
    return startOfLastMonth.ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfNextMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    var nextMonth = dateTimeOffset.AddMonths(1);
    var dateOnly = new DateTime(nextMonth.Year, nextMonth.Month, 1, 0, 0, 0,
      DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfNextMonth = new DateTimeOffset(dateOnly, offset);
    return startOfNextMonth;
  }

  public static DateTimeOffset GetStartOfDay(this DateTimeOffset dateTimeOffset)
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month,
      dateTimeOffset.Day, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfDay = new DateTimeOffset(dateOnly, offset);
    return startOfDay.ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfYear(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, 1, 1, 0, 0, 0,
      DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfYear = new DateTimeOffset(dateOnly, offset);
    return startOfYear.ToUniversalTime();
  }

  public static DateTimeOffset GetStartOfYearNoTransformation(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, 1, 1, 0, 0, 0,
      DateTimeKind.Unspecified);

    var startOfYear = new DateTimeOffset(dateOnly, TimeSpan.Zero);
    return startOfYear.ToUniversalTime();
  }

  public static IEnumerable<DateTimeOffset>
    GetThisYearMonthStartsNoTransformation(
      this DateTimeOffset dateTimeOffset
    )
  {
    var year = dateTimeOffset.Year;

    return Enumerable.Range(1, 12).Select(month =>
    {
      var dateOnly =
        new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Unspecified);
      var startOfMonth = new DateTimeOffset(dateOnly, TimeSpan.Zero);
      return startOfMonth.ToUniversalTime();
    });
  }
}
