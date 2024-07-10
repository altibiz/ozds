namespace Ozds.Business.Test;

public static class Constants
{
  public static readonly DateTimeOffset DefaultDateTimeOffset =
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      System.Globalization.CultureInfo.InvariantCulture);

  public const int DefaultFuzzCount = 100;

  public const decimal MinEnergyValue = 0;

  public const decimal MaxEnergyValue = uint.MaxValue;

  public const decimal MinTotalValue = 0;

  public const decimal MaxTotalValue = uint.MaxValue;
}
