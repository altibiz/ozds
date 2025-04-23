using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.JSInterop;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Extensions;
using Ozds.Client.State;

namespace Ozds.Client.Components.Base;

// NOTE: to be used inside ErrorBoundary, ScopeStateProvider
// and CultureStateProvider
public abstract class OzdsComponentBase : DisposableComponentBase
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  [CascadingParameter]
  private CultureState CultureState { get; set; } = default!;

  [CascadingParameter]
  private ScopeState ScopeState { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  [Inject]
  private TemplateBinderFactory TemplateBinderFactory { get; set; } = default!;

  protected string Href
  {
    get { return new Uri(NavigationManager.Uri).AbsolutePath; }
  }

  protected string LoginHref
  {
    get { return $"/login?returnUrl={Uri.EscapeDataString(Href)}"; }
  }

  protected string LogoutHref
  {
    get
    {
      return
        $"/users/logoff?returnUrl=/login?returnUrl={Uri.EscapeDataString(Href)}";
    }
  }

  protected string IndexHref
  {
    get { return BasedHref("/"); }
  }

  protected IServiceProvider ScopedServices
  {
    get
    {
      return ScopeState?.ScopedServices ??
        throw new InvalidOperationException($"{this} got disposed");
    }
  }

  protected CultureInfo CroatianCulture
  {
    get
    {
      var localizationQueries = ScopedServices
        .GetRequiredService<LocalizationQueries>();
      return localizationQueries.CroatianCulture;
    }
  }

  protected CultureInfo EnglishCulture
  {
    get
    {
      var localizationQueries = ScopedServices
        .GetRequiredService<LocalizationQueries>();
      return localizationQueries.EnglishCulture;
    }
  }

  protected string NumericString(decimal? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var cultureInfo = localizationQueries.CroatianCulture;

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  protected string NumericString(float? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var cultureInfo = localizationQueries.CroatianCulture;

    var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
    numberFormatInfo.NumberGroupSeparator = ".";
    numberFormatInfo.NumberDecimalDigits = places;

    var roundedNumber = Math.Round(number.Value, places);
    return roundedNumber.ToString("N", numberFormatInfo);
  }

  protected string DateString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var cultureInfo = localizationQueries.CroatianCulture;

    var withTimezone = dateTimeOffset
      .Value
      .ToOffset(DateTimeOffsetExtensions.GetOffset(dateTimeOffset.Value));

    return withTimezone.ToString("dd. MM. yyyy.", cultureInfo);
  }

  protected string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var cultureInfo = localizationQueries.CroatianCulture;

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

  protected string Translate(string notLocalized)
  {
    var culture = GetCulture();
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.Translate(culture, notLocalized);
  }

  protected string Translate(Type type)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.Translate(culture, type);
  }

  protected string Translate(Type type, string member)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.Translate(culture, type, member);
  }

  protected string Translate(MemberExpression member)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.Translate(culture, member);
  }

  protected static string JsonString(object? jsonDocument)
  {
    return JsonSerializer.Serialize(jsonDocument, JsonSerializerOptions);
  }

  protected string PageHref<T>(object? parameters = null)
  {
    return PageHref(typeof(T), parameters);
  }

  protected string PageHref(
    Type type,
    object? parameters = null
  )
  {
    return NavigationManager.PageHref(
      TemplateBinderFactory,
      type,
      parameters
    );
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
    NavigationManager.NavigateTo(PageHref<T>(parameters));
  }

  protected void NavigateToPage(Type type, object? parameters = null)
  {
    NavigationManager.NavigateTo(PageHref(type, parameters));
  }

  private string BasedHref(string uri)
  {
    return NavigationManager.BasedHref(uri);
  }

  protected CultureInfo GetCulture()
  {
    if (CultureState?.Culture is { } culture)
    {
      return culture;
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.EnglishCulture;
  }

  protected Task SetCulture(CultureInfo culture)
  {
    if (CultureState is { } cultureState)
    {
      return cultureState.SetCulture(culture);
    }

    return Task.CompletedTask;
  }
}
