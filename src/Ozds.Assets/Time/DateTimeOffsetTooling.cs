namespace Ozds.Assets.Time;

public static class DateTimeOffsetTooling
{
  public static TimeSpan GetOffset(DateTimeOffset forDate)
  {
    return TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb")
      .GetUtcOffset(forDate);
  }
}
