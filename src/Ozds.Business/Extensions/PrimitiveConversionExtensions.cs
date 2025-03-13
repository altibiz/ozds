namespace Ozds.Business.Extensions;

public static class PrimitiveConversionExtensions
{
  public static float ToFloat(this decimal value)
  {
    try
    {
      return (float)value;
    }
    catch (OverflowException)
    {
      return 0;
    }
  }

  public static double ToDouble(this decimal value)
  {
    try
    {
      return (long)value;
    }
    catch (OverflowException)
    {
      return 0;
    }
  }

  public static long ToLong(this decimal value)
  {
    try
    {
      return (long)value;
    }
    catch (OverflowException)
    {
      return 0;
    }
  }

  public static decimal ToDecimal(this float value)
  {
    try
    {
      return (decimal)value;
    }
    catch (OverflowException)
    {
      return 0;
    }
  }

  public static decimal ToDecimal(this double value)
  {
    try
    {
      return (decimal)value;
    }
    catch (OverflowException)
    {
      return 0;
    }
  }

  public static decimal ToDecimal(this long value)
  {
    try
    {
#pragma warning disable IDE0004
      return value;
#pragma warning restore IDE0004
    }
    catch (OverflowException)
    {
      return 0;
    }
  }
}
