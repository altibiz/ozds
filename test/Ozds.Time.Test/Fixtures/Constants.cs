using System.Globalization;

namespace Ozds.Time.Test.Fixtures;

public static class Constants
{
  public static readonly DateTimeOffset DefaultDateTimeOffset =
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture);
}
