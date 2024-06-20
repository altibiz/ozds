using System.Globalization;
using Fluid.Ast.BinaryExpressions;

namespace Ozds.Business.Time;

public static class DateTimeOffsetExtensions
{
  // NOTE: Croatian UTC offset (https://en.wikipedia.org/wiki/List_of_UTC_offsets)

  public static TimeSpan DefaultOffset = TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb").GetUtcOffset(DateTime.UtcNow);
  private static TimeSpan GetOffset(DateTimeOffset forDate)
  {
    return TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb").GetUtcOffset(forDate);
  }
  // private static readonly Dictionary<string, string> CultureTimeZoneMap = new Dictionary<string, string>
  //   {
  //       { "en-US", "Eastern Standard Time" },
  //       { "hr-HR", "Central European Standard Time" },
  //   };
  // public static void GetTimeOffsetForCurrentCulture()
  // {
  //   var currentCulture = CultureInfo.CurrentCulture.Name;

  //   if (CultureTimeZoneMap.TryGetValue(currentCulture, out string? timeZoneId))
  //   {
  //     var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

  //     var offset = timeZone.GetUtcOffset(DateTime.UtcNow);

  //     DefaultOffset = offset;
  //   }
  //   else
  //   {
  //     var localTimeZone = TimeZoneInfo.Local;
  //     DefaultOffset = localTimeZone.GetUtcOffset(DateTime.UtcNow);
  //   }
  // }
  // public static readonly TimeSpan OldDefaultOffset = TimeSpan.FromHours(1);


  public static (DateTimeOffset, DateTimeOffset) GetMonthRange(
    this DateTimeOffset dateTimeOffset
  )
  {

    var dateOnlyStart = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offsetStart = GetOffset(dateOnlyStart);
    var dateOnlyEnd = new DateTime(dateTimeOffset.Year, dateTimeOffset.AddMonths(1).Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offsetEnd = GetOffset(dateOnlyEnd);

    var start = new DateTimeOffset(dateOnlyStart, offsetStart);
    var end = new DateTimeOffset(dateOnlyEnd, offsetEnd);
    return (start.UtcDateTime, end.UtcDateTime);

    // var a = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset));
    // var monthStart = new DateTimeOffset(
    //   a.Year,
    //   a.Month,
    //   1,
    //   0,
    //   0,
    //   0,
    //   a.Offset
    // );
    // var b = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset.AddMonths(1)));
    // var nextMonthStart = new DateTimeOffset(
    //   b.Year,
    //   b.Month,
    //   1,
    //   0,
    //   0,
    //   0,
    //   b.Offset
    // );
    // return (monthStart, nextMonthStart);
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

    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfMonth = new DateTimeOffset(dateOnly, offset);
    return startOfMonth.UtcDateTime;
  }

  public static DateTimeOffset GetStartOfLastMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.AddMonths(-1).Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfLastMonth = new DateTimeOffset(dateOnly, offset);
    return startOfLastMonth.UtcDateTime;

    // dateTimeOffset = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset.AddMonths(-1)));
    // return new DateTimeOffset(
    //   dateTimeOffset.Year,
    //   dateTimeOffset.Month,
    //   1,
    //   0,
    //   0,
    //   0,
    //   dateTimeOffset.Offset
    // );
  }

  public static DateTimeOffset GetStartOfNextMonth(
    this DateTimeOffset dateTimeOffset
  )
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.AddMonths(1).Month, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfNextMonth = new DateTimeOffset(dateOnly, offset);
    return startOfNextMonth.UtcDateTime;

    // dateTimeOffset = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset.AddMonths(1)));
    // return new DateTimeOffset(
    //   dateTimeOffset.Year,
    //   dateTimeOffset.Month,
    //   1,
    //   0,
    //   0,
    //   0,
    //   dateTimeOffset.Offset
    // );
  }

  public static DateTimeOffset GetStartOfDay(this DateTimeOffset dateTimeOffset)
  {
    var dateOnly = new DateTime(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfDay = new DateTimeOffset(dateOnly, offset);
    return startOfDay.UtcDateTime;
  }

  public static DateTimeOffset GetStartOfYear(
    this DateTimeOffset dateTimeOffset
  )
  {

    var dateOnly = new DateTime(dateTimeOffset.Year, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
    var offset = GetOffset(dateOnly);

    var startOfYear = new DateTimeOffset(dateOnly, offset);
    return startOfYear.UtcDateTime;

    // dateTimeOffset = dateTimeOffset.ToOffset(GetOffset(dateTimeOffset.AddMonths(-dateTimeOffset.Month)));
    // return new DateTimeOffset(
    //   dateTimeOffset.Year,
    //   1,
    //   1,
    //   0,
    //   0,
    //   0,
    //   dateTimeOffset.Offset
    // );
  }
  // ToDo FIX THIS
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
          ).ToUniversalTime()

      );
  }
}
