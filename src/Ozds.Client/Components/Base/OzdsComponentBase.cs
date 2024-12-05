using System.Globalization;
using System.Reflection;
using System.Text.Json;
using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;
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
  private TemplateBinderFactory TemplateBinderFactory { get; set; } = default!;

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

  protected string LoginHref =>
    $"/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}";

  protected string LogoutHref =>
    $"/logout?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}";

  protected string IndexHref =>
    BasedHref("/");

  protected string PageHref<T>(object? parameters = null) =>
    PageHref(typeof(T), parameters);

  protected string PageHref(Type type, object? parameters = null)
  {
    var attribute = type.GetCustomAttribute<RouteAttribute>()
      ?? throw new InvalidOperationException(
        $"{type} is not decorated with {nameof(RouteAttribute)}");
    var route = attribute.Template;
    var template = TemplateParser.Parse(route);
    var pattern = RoutePatternFactory.Parse(route);
    var values = new RouteValueDictionary(parameters);
    var uri = TemplateBinderFactory
      .Create(template, new RouteValueDictionary(pattern.Defaults))
      .BindValues(values)
        ?? throw new InvalidOperationException(
          $"{type} has no route template");
    return BasedHref(uri);
  }

  protected void NavigateBack()
  {
#pragma warning disable CA2012 // Use ValueTasks correctly
    JS.InvokeVoidAsync("history.back");
#pragma warning restore CA2012 // Use ValueTasks correctly
  }

  protected void NavigateToLogin()
  {
    NavigationManager.NavigateTo(LoginHref);
  }

  protected void NavigateToLogout()
  {
    NavigationManager.NavigateTo(LogoutHref);
  }

  protected void NavigateToIndex()
  {
    NavigationManager.NavigateTo(IndexHref);
  }

  protected void NavigateToPage<T>(object? parameters = null)
  {
    NavigateToPage(typeof(T), parameters);
  }

  protected void NavigateToPage(Type type, object? parameters = null)
  {
    NavigationManager.NavigateTo(PageHref(type, parameters));
  }

  private string BasedHref(string uri)
  {
    var @base = new Uri(NavigationManager.BaseUri).AbsolutePath;
    return @base + uri.TrimStart('/');
  }
}
