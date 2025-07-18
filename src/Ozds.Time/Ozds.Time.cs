namespace Ozds.Time;

// NOTE: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones

public static class TimeConstants
{
  public static readonly TimeZoneInfo CroatianTimeZone =
    TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb");

  public static readonly TimeZoneInfo UtcTimeZone =
    TimeZoneInfo.FindSystemTimeZoneById("Etc/UTC");
}
