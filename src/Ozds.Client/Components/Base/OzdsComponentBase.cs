using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.JSInterop;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Time;

namespace Ozds.Client.Components.Base;

public abstract class OzdsComponentBase : ComponentBase
{
  private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  [Inject]
  private ILocalizer Localizer { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  [Inject]
  private LinkGenerator LinkGenerator { get; set; } = default!;

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

  protected string Translate(string notLocalized)
  {
    var localized = Localizer.Translate(notLocalized);
    return localized;
  }

  protected void NavigateToLogin()
  {
    NavigationManager.NavigateTo(
      $"/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
  }

  protected void NavigateToIndex()
  {
    NavigationManager.NavigateTo("/");
  }

  protected void NavigateBack()
  {
    JS.InvokeVoidAsync("history.back");
  }

  protected void NavigateToPage<T>(object? parameters = null)
  {
    var attribute = typeof(T).GetCustomAttribute<RouteAttribute>();
    if (attribute is null)
    {
      throw new InvalidOperationException(
        $"{typeof(T)} is not decorated with {nameof(RouteAttribute)}");
    }

    var route = attribute.Template;
    if (parameters is null)
    {
      NavigationManager.NavigateTo(route);
    }

    var uri = LinkGenerator.GetPathByAddress(
      route,
      values: new RouteValueDictionary(parameters),
      pathBase: new PathString("/"));
    if (uri is null)
    {
      throw new InvalidOperationException(
        $"{typeof(T)} has no route template");
    }

    NavigationManager.NavigateTo(uri);
  }
}
