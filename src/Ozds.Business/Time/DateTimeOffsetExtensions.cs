namespace Ozds.Business.Time;

public static class DateTimeOffsetExtensions
{
  // NOTE: https://en.wikipedia.org/wiki/List_of_UTC_offsets
  private static readonly TimeZoneInfo CroatianTimeZone =
    TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb");

  public static TimeSpan GetOffset(DateTimeOffset forDate)
  {
    return CroatianTimeZone.GetUtcOffset(forDate);
  }

  public static (DateTimeOffset, DateTimeOffset) GetMonthRange(
    this DateTimeOffset dateTimeOffset
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
      TimeZoneInfo.ConvertTimeToUtc(localStartOfMonth, CroatianTimeZone);

    var localStartOfNextMonth = localStartOfMonth.AddMonths(1);

    var utcStartOfNextMonth =
      TimeZoneInfo.ConvertTimeToUtc(localStartOfNextMonth, CroatianTimeZone);

    return (
      new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero),
      new DateTimeOffset(utcStartOfNextMonth, TimeSpan.Zero)
    );
  }

  public static DateTimeOffset GetStartOfQuarterHour(
    this DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
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
      localStartOfQuarterHour, CroatianTimeZone);

    return new DateTimeOffset(utcStartOfQuarterHour, TimeSpan.Zero);
  }

  public static DateTimeOffset GetStartOfMonth(
    this DateTimeOffset dateTimeOffset
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
      TimeZoneInfo.ConvertTimeToUtc(localStartOfMonth, CroatianTimeZone);

    return new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero);
  }

  public static DateTimeOffset GetStartOfLastMonth(
    this DateTimeOffset dateTimeOffset
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
      TimeZoneInfo.ConvertTimeToUtc(localStartOfLastMonth, CroatianTimeZone);

    return new DateTimeOffset(utcStartOfLastMonth, TimeSpan.Zero);
  }

  public static DateTimeOffset GetStartOfNextMonth(
    this DateTimeOffset dateTimeOffset
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
      TimeZoneInfo.ConvertTimeToUtc(localStartOfNextMonth, CroatianTimeZone);

    return new DateTimeOffset(utcStartOfNextMonth, TimeSpan.Zero);
  }

  public static DateTimeOffset GetStartOfDay(
    this DateTimeOffset dateTimeOffset
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
      TimeZoneInfo.ConvertTimeToUtc(localStartOfDay, CroatianTimeZone);

    return new DateTimeOffset(utcStartOfDay, TimeSpan.Zero);
  }

  public static DateTimeOffset GetStartOfYear(
    this DateTimeOffset dateTimeOffset
  )
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var localStartOfYear = new DateTime(
      localDateTime.Year, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

    var utcStartOfYear =
      TimeZoneInfo.ConvertTimeToUtc(localStartOfYear, CroatianTimeZone);
    return new DateTimeOffset(utcStartOfYear, TimeSpan.Zero);
  }

  public static IEnumerable<DateTimeOffset> GetThisYearMonthStarts(
    this DateTimeOffset dateTimeOffset)
  {
    var localDateTime = TimeZoneInfo.ConvertTime(
      dateTimeOffset,
      CroatianTimeZone
    );

    var year = localDateTime.Year;

    return Enumerable.Range(1, 12).Select(
      month =>
      {
        var localStartOfMonth = new DateTime(
          year, month, 1, 0, 0, 0, DateTimeKind.Unspecified);

        var utcStartOfMonth =
          TimeZoneInfo.ConvertTimeToUtc(localStartOfMonth, CroatianTimeZone);

        return new DateTimeOffset(utcStartOfMonth, TimeSpan.Zero);
      });
  }
}
