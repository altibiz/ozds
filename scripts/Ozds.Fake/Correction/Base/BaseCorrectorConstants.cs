namespace Ozds.Fake.Correction.Base;

public static class BaseCorrectorConstants
{
  public const decimal EpsilonPrecision = 2;

  public static readonly decimal EpsilonMultiplier = (decimal)
    Math.Pow(10, (double)EpsilonPrecision);

  public static readonly decimal EpsilonValue = 1 / EpsilonMultiplier;
}
