using System.Globalization;

namespace Ozds.Assets;

public static class AssetConstants
{
  public static readonly TimeZoneInfo CroatianTimeZone =
    TimeZoneInfo.FindSystemTimeZoneById("Europe/Zagreb");

  public static readonly CultureInfo CroatianCulture = new("hr-HR");

  public static readonly CultureInfo EnglishCulture = new("en-US");
}
