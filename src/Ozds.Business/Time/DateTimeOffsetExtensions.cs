using System.Globalization;

namespace Ozds.Business.Time;

public static class DateTimeOffsetExtensions
{
  // NOTE: Croatian UTC offset (https://en.wikipedia.org/wiki/List_of_UTC_offsets)

  public static TimeSpan DefaultOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
  private static readonly Dictionary<string, string> CultureTimeZoneMap = new Dictionary<string, string>
    {
        { "en-US", "Eastern Standard Time" },
        { "hr-HR", "Central European Standard Time" },
    };
  public static void GetTimeOffsetForCurrentCulture()
  {
    var currentCulture = CultureInfo.CurrentCulture.Name;

    if (CultureTimeZoneMap.TryGetValue(currentCulture, out string? timeZoneId))
    {
      var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

      var offset = timeZone.GetUtcOffset(DateTime.UtcNow);

      DefaultOffset = offset;
    }
    else
    {
      var localTimeZone = TimeZoneInfo.Local;
      DefaultOffset = localTimeZone.GetUtcOffset(DateTime.UtcNow);
    }
  }
  public static readonly TimeSpan OldDefaultOffset = TimeSpan.FromHours(1);


  public static (DateTimeOffset, DateTimeOffset) GetMonthRange(
    this DateTimeOffset dateTimeOffset
  )
  {
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
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
    GetTimeOffsetForCurrentCulture();
    dateTimeOffset = dateTimeOffset.ToOffset(DefaultOffset);
    return Enumerable
      .Range(1, 12).Select(
        month =>
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
