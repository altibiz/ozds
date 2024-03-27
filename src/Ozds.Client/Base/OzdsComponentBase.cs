using System.Globalization;
using Ozds.Business.Time;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Base;

public abstract class OzdsComponentBase : ComponentBase
{
  public OzdsComponentLocalizer T => new();
  protected string DecimalString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = new CultureInfo("hr-HR");

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    decimal roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  protected string DateString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var cultureInfo = new CultureInfo("hr-HR");

    var withTimezone = dateTimeOffset
      .Value
      .ToOffset(DateTimeOffsetExtensions.DefaultOffset);

    return withTimezone.ToString("dd. MM. yyyy.", cultureInfo);
  }
  protected DateTimeOffset DateTimeGraph(DateTimeOffset dateTimeOffset)
  {
    return dateTimeOffset.UtcDateTime.Add(DateTimeOffsetExtensions.DefaultOffset);
  }
}
