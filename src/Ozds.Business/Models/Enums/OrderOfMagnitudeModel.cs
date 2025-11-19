using Ozds.Business.Extensions;

namespace Ozds.Business.Models.Enums;

public enum OrderOfMagnitudeModel
{
  Giga = 9,
  Mega = 6,
  Kilo = 3,
  Hecto = 2,
  Deca = 1,
  Deci = -1,
  Centi = -2,
  Milli = -3,
  Micro = -6,
  Nano = -9
}

public static class OrderOfMagnitudeModelExtensions
{
  public static decimal ToMultiplier(this OrderOfMagnitudeModel? order)
  {
    return order switch
    {
      OrderOfMagnitudeModel.Giga => System.Math.Pow(10, -9).ToDecimal(),
      OrderOfMagnitudeModel.Mega => System.Math.Pow(10, -6).ToDecimal(),
      OrderOfMagnitudeModel.Kilo => System.Math.Pow(10, -3).ToDecimal(),
      OrderOfMagnitudeModel.Hecto => System.Math.Pow(10, -2).ToDecimal(),
      OrderOfMagnitudeModel.Deca => System.Math.Pow(10, -1).ToDecimal(),
      null => 1,
      OrderOfMagnitudeModel.Deci => System.Math.Pow(10, 1).ToDecimal(),
      OrderOfMagnitudeModel.Centi => System.Math.Pow(10, 2).ToDecimal(),
      OrderOfMagnitudeModel.Milli => System.Math.Pow(10, 3).ToDecimal(),
      OrderOfMagnitudeModel.Micro => System.Math.Pow(10, 6).ToDecimal(),
      OrderOfMagnitudeModel.Nano => System.Math.Pow(10, 9).ToDecimal(),
      _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
    };
  }

  public static string ToTitle(this OrderOfMagnitudeModel? order)
  {
    return order switch
    {
      OrderOfMagnitudeModel.Giga => "Giga",
      OrderOfMagnitudeModel.Mega => "Mega",
      OrderOfMagnitudeModel.Kilo => "Kilo",
      OrderOfMagnitudeModel.Hecto => "Hecto",
      OrderOfMagnitudeModel.Deca => "Deca",
      null => "",
      OrderOfMagnitudeModel.Deci => "Deci",
      OrderOfMagnitudeModel.Centi => "Centi",
      OrderOfMagnitudeModel.Milli => "Milli",
      OrderOfMagnitudeModel.Micro => "Micro",
      OrderOfMagnitudeModel.Nano => "Nano",
      _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
    };
  }

  public static string ToPrefix(this OrderOfMagnitudeModel? order)
  {
    return order switch
    {
      OrderOfMagnitudeModel.Giga => "G",
      OrderOfMagnitudeModel.Mega => "M",
      OrderOfMagnitudeModel.Kilo => "k",
      OrderOfMagnitudeModel.Hecto => "h",
      OrderOfMagnitudeModel.Deca => "da",
      null => "",
      OrderOfMagnitudeModel.Deci => "d",
      OrderOfMagnitudeModel.Centi => "c",
      OrderOfMagnitudeModel.Milli => "m",
      OrderOfMagnitudeModel.Micro => "μ",
      OrderOfMagnitudeModel.Nano => "n",
      _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
    };
  }

  public static OrderOfMagnitudeModel ToOrderOfMagnitudeFromTitle(
    this string title)
  {
    return title switch
    {
      "Giga" => OrderOfMagnitudeModel.Giga,
      "Mega" => OrderOfMagnitudeModel.Mega,
      "Kilo" => OrderOfMagnitudeModel.Kilo,
      "Hecto" => OrderOfMagnitudeModel.Hecto,
      "Deca" => OrderOfMagnitudeModel.Deca,
      "Deci" => OrderOfMagnitudeModel.Deci,
      "Centi" => OrderOfMagnitudeModel.Centi,
      "Milli" => OrderOfMagnitudeModel.Milli,
      "Micro" => OrderOfMagnitudeModel.Micro,
      "Nano" => OrderOfMagnitudeModel.Nano,
      _ => throw new ArgumentOutOfRangeException(nameof(title), title, null)
    };
  }

  public static OrderOfMagnitudeModel ToOrderOfMagnitudeFromPrefix(
    this string prefix)
  {
    return prefix switch
    {
      "G" => OrderOfMagnitudeModel.Giga,
      "M" => OrderOfMagnitudeModel.Mega,
      "k" => OrderOfMagnitudeModel.Kilo,
      "h" => OrderOfMagnitudeModel.Hecto,
      "da" => OrderOfMagnitudeModel.Deca,
      "d" => OrderOfMagnitudeModel.Deci,
      "c" => OrderOfMagnitudeModel.Centi,
      "m" => OrderOfMagnitudeModel.Milli,
      "μ" => OrderOfMagnitudeModel.Micro,
      "n" => OrderOfMagnitudeModel.Nano,
      _ => throw new ArgumentOutOfRangeException(nameof(prefix), prefix, null)
    };
  }
}
