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
}
