using System.Globalization;

namespace Ozds.Business.Test.Fixtures;

public static class Constants
{
  public const int DefaultFuzzCount = 100;

  public const decimal MinEnergyValue = 0;

  public const decimal MaxEnergyValue = uint.MaxValue;

  public const decimal MinPowerValue = 0;

  public const decimal MaxPowerValue = uint.MaxValue;

  public const decimal MinTotalValue = 0;

  public const decimal MaxTotalValue = uint.MaxValue;

  public static readonly DateTimeOffset DefaultDateTimeOffset =
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture);
}
