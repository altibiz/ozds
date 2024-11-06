using System.Globalization;

namespace Ozds.Data.Test.Fixtures;

public static class Constants
{
  public const int DefaultFuzzCount = 8;

  public const int DefaultRepeatCount = 3;

  public static readonly DateTimeOffset DefaultDateTimeOffset =
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture);
}
