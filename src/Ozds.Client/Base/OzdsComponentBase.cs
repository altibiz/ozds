using System.Globalization;
using ApexCharts;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Time;

namespace Ozds.Client.Base;

public abstract class OzdsComponentBase : ComponentBase
{
  [Inject] public IOzdsComponentLocalizer T { get; set; } = default!;

  [Inject] public NavigationManager NavigationManager { get; set; } = default!;

  protected override void OnInitialized()
  {
    var uri = new Uri(NavigationManager.Uri);
    var culture = (uri.Segments[2]
                   ?? throw new InvalidOperationException("Culture not found"))
      .TrimEnd('/');
    SetCulture(culture);
  }

  private static void SetCulture(string culture)
  {
    try
    {
      var ci = CultureInfo.GetCultureInfo(culture);
      CultureInfo.DefaultThreadCurrentCulture = ci;
      CultureInfo.DefaultThreadCurrentUICulture = ci;
    }
    catch (CultureNotFoundException)
    {
      var defaultCulture = new CultureInfo("en-US");
      CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
      CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
    }
  }

  public static string GetCulture()
  {
    return CultureInfo.CurrentCulture.Name;
  }

  public static ApexChartOptions<T> NewApexChartOptions<T>() where T : class
  {
    var options = new ApexChartOptions<T>
    {
      Blazor = new ApexChartsBlazorOptions
      {
        JavascriptPath = "/_content/Blazor-ApexCharts/js/blazor-apexcharts.js"
      }
    };

    return options;
  }

  protected static string DecimalString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = new CultureInfo("hr-HR");

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  protected static string FloatString(float? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var cultureInfo = new CultureInfo("hr-HR");

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  protected static string DateString(DateTimeOffset? dateTimeOffset)
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

  protected static string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var cultureInfo = new CultureInfo("hr-HR");

    var withTimezone = dateTimeOffset
      .Value
      .ToOffset(DateTimeOffsetExtensions.DefaultOffset);

    return withTimezone.ToString("dd. MM. yyyy. HH:mm", cultureInfo);
  }


  protected static DateTimeOffset DateTimeGraph(DateTimeOffset dateTimeOffset)
  {
    return dateTimeOffset.UtcDateTime.Add(
      DateTimeOffsetExtensions.DefaultOffset);
  }
}
