using System.Globalization;
using System.Reflection;
using System.Text.Json;
using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Time;
using Ozds.Client.Attributes;

namespace Ozds.Client.Components.Base;

public abstract class OzdsComponentBase : ComponentBase
{
  private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  private CultureInfo? _culture;

  [Inject]
  public ILocalizer T { get; set; } = default!;

  [Inject]
  public NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  protected override void OnInitialized()
  {
    var uri = new Uri(NavigationManager.Uri);
    var culture = (uri.Segments[2]
        ?? throw new InvalidOperationException("Culture not found"))
      .TrimEnd('/');
    SetCulture(culture);
  }

  protected string Translate(string notLocalized) => T.Translate(notLocalized);

  protected CultureInfo Culture
  {
    get
    {
      if (_culture is not null)
      {
        return _culture;
      }

      var uri = new Uri(NavigationManager.Uri);
      var culture = uri.Segments[2]?.TrimEnd('/') ?? "en-US";
      var ci = CultureInfo.GetCultureInfo(culture);
      return _culture = ci;
    }
#pragma warning disable S4275 // Getters and setters should access the expected fields
    set
#pragma warning restore S4275 // Getters and setters should access the expected fields
    {
      var uri = new Uri(NavigationManager.Uri);
      var segments = uri.Segments;
      segments[2] = $"{value}/";
      var path = string.Join("", segments);
      CultureInfo.DefaultThreadCurrentCulture = value;
      CultureInfo.DefaultThreadCurrentUICulture = value;
      NavigationManager.NavigateTo(path);
    }
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

  public static ApexChartOptions<T> NewApexChartOptions<T>()
    where T : class
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

  protected static string NumericString(decimal? number, int places = 2)
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

  protected static string NumericString(float? number, int places = 2)
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
      .ToOffset(DateTimeOffsetExtensions.GetOffset(dateTimeOffset.Value));

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
      .ToOffset(DateTimeOffsetExtensions.GetOffset(dateTimeOffset.Value));

    return withTimezone.ToString("dd. MM. yyyy. HH:mm", cultureInfo);
  }

  protected static DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    var a = dateTimeOffset.UtcDateTime.Add(
      DateTimeOffsetExtensions.GetOffset(dateTimeOffset));
    return a;
  }

  protected static string JsonString(object? jsonDocument)
  {
    return JsonSerializer.Serialize(jsonDocument, _jsonSerializerOptions);
  }

  protected void NavigateToLogin()
  {
    NavigationManager.NavigateTo(
      $"/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
  }

  protected void NavigateBack()
  {
    JS.InvokeVoidAsync("history.back");
  }
}
