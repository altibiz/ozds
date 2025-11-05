namespace Ozds.Business.Extensions;

public static class PrimitiveConversionExtensions
{
  public static float ToFloat(this decimal value)
  {
    return Convert.ToSingle(value);
  }

  public static double ToDouble(this decimal value)
  {
    return Convert.ToDouble(value);
  }

  public static long ToLong(this decimal value)
  {
    return value > long.MaxValue
      ? long.MaxValue
      : value < long.MinValue
      ? long.MinValue
      : Convert.ToInt64(value);
  }

  public static decimal ToDecimal(this float value)
  {
    return Convert.ToDecimal(value);
  }

  public static decimal ToDecimal(this double value)
  {
    return Convert.ToDecimal(value);
  }

  public static decimal ToDecimal(this long value)
  {
    return Convert.ToDecimal(value);
  }
}
