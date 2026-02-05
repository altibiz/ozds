using System.Globalization;

namespace Ozds.Data.Test.Fixtures;

public static class Constants
{
  public const int MinAggregateValue = 0;

  public const int MaxAggregateValue = 100;

  public const int DefaultDbFuzzCount = 3;

  public const string NowString = "2024-10-27T05:15:00Z";

  public const string NowStartOfQuarterHourString = "2024-10-27T05:15:00Z";

  public const string NowStartOfDayString = "2024-10-26T22:00:00Z";

  public const string NowStartOfMonthString = "2024-09-30T22:00:00Z";

  public const int MassiveMeasurementCount = 10000 / DefaultDbFuzzCount / 2;

  public const int MeasurementCount = 1000 / DefaultDbFuzzCount / 2;

  public const int AggregateCount = MeasurementCount / 10;

  public const int MeasurementCountFew = MeasurementCount / 4;

  public const int AggregateCountFew = MeasurementCountFew / 10;

  public static readonly DateTimeOffset Now = new(
    DateTime.SpecifyKind(
      DateTimeOffset.Parse(NowString, CultureInfo.InvariantCulture).UtcDateTime,
      DateTimeKind.Utc
    ),
    TimeSpan.Zero
  );

  public static readonly DateTimeOffset NowStartOfQuarterHour = new(
    DateTime.SpecifyKind(
      DateTimeOffset
        .Parse(NowStartOfQuarterHourString, CultureInfo.InvariantCulture)
        .UtcDateTime,
      DateTimeKind.Utc
    ),
    TimeSpan.Zero
  );

  public static readonly DateTimeOffset NowStartOfDay = new(
    DateTime.SpecifyKind(
      DateTimeOffset
        .Parse(NowStartOfDayString, CultureInfo.InvariantCulture)
        .UtcDateTime,
      DateTimeKind.Utc
    ),
    TimeSpan.Zero
  );

  public static readonly DateTimeOffset NowStartOfMonth = new(
    DateTime.SpecifyKind(
      DateTimeOffset
        .Parse(NowStartOfMonthString, CultureInfo.InvariantCulture)
        .UtcDateTime,
      DateTimeKind.Utc
    ),
    TimeSpan.Zero
  );
}
