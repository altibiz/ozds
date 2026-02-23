using AngleSharp.Io;

namespace Ozds.Business.Extensions;

public static class PrimitiveRoundingExtensions
{
  public static decimal Round(decimal value, int digits)
  {
    return System.Math.Round(value, digits, MidpointRounding.AwayFromZero);
  }

  public static double Round(double value, int digits)
  {
    return System.Math.Round(value, digits, MidpointRounding.AwayFromZero);
  }
}
