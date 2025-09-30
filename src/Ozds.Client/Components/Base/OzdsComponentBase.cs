using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.JSInterop;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
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

  protected string BaseHref
  {
    get { return new Uri(NavigationManager.BaseUri).AbsolutePath; }
  }

  protected string LoginHref
  {
    get
    {
      return $"/app/auth/login?returnUrl={Uri.EscapeDataString(BaseHref)}";
    }
  }

  protected string LogoutHref
  {
    get
    {
      return "/app/auth/logout"
        + $"?returnUrl={Uri.EscapeDataString(BaseHref)}/auth/login"
        + $"?returnUrl={Uri.EscapeDataString(BaseHref)}";
    }
  }

  protected string IndexHref
  {
    get { return BasedHref("/"); }
  }

  protected string ApiV1Href
  {
    get { return "/api/v1/openapi"; }
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
    return localizationQueries.NumericString(number, places);
  }

  protected string NumericString(float? number, int places = 2)
  {
    if (number is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.NumericString(number, places);
  }

  protected string DateString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.DateString(dateTimeOffset);
  }

  protected string DateTimeString(DateTimeOffset? dateTimeOffset)
  {
    if (dateTimeOffset is null)
    {
      return "";
    }

    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.DateTimeString(dateTimeOffset);
  }

  protected DateTimeOffset DateTimeApplyOffset(
    DateTimeOffset dateTimeOffset)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.DateTimeApplyOffset(dateTimeOffset);
  }

  protected string Translate(string notLocalized)
  {
    var culture = GetCulture();
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    return localizationQueries.Translate(culture, notLocalized);
  }

  protected string Translate(Type type, bool plural = false)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.Translate(culture, type, plural);
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

  protected string TranslateDuration(
    DurationModel duration,
    bool plural = false
  )
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.TranslateDuration(
      culture,
      duration,
      plural);
  }

  protected string TranslatePeriod(
    PeriodModel period
  )
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.TranslatePeriod(
      culture,
      period);
  }

  protected string DateFormat()
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.DateFormat(culture);
  }

  protected string DateTimeFormat()
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var culture = GetCulture();
    return localizationQueries.DateTimeFormat(culture);
  }

  protected static string JsonString(object? jsonDocument)
  {
    return JsonSerializer.Serialize(jsonDocument, JsonSerializerOptions);
  }

  protected string PageHref<T>(
    object? parameters = null,
    object? queryParameters = null
  )
  {
    return PageHref(typeof(T), parameters, queryParameters);
  }

  protected string PageHref(
    Type type,
    object? parameters = null,
    object? queryParameters = null
  )
  {
    return NavigationManager.PageHref(
      TemplateBinderFactory,
      type,
      parameters,
      queryParameters
    );
  }

  protected void NavigateBack()
  {
#pragma warning disable CA2012 // Use ValueTasks correctly
    JS.InvokeVoidAsync("history.back");
#pragma warning restore CA2012 // Use ValueTasks correctly
  }

  protected void NavigateHere()
  {
#pragma warning disable CA2012 // Use ValueTasks correctly
    JS.InvokeVoidAsync("location.reload");
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

  protected void NavigateToApiV1()
  {
    NavigationManager.NavigateTo(ApiV1Href);
  }

  protected void NavigateToPage<T>(
    object? parameters = null,
    object? queryParameters = null
  )
  {
    NavigationManager.NavigateTo(PageHref<T>(parameters));
  }

  protected void NavigateToPage(
    Type type,
    object? parameters = null,
    object? queryParameters = null
  )
  {
    NavigationManager.NavigateTo(PageHref(type, parameters, queryParameters));
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

  protected TimeZoneInfo GetTimeZone()
  {
    if (CultureState is { } cultureState)
    {
      return cultureState.TimeZoneInfo;
    }

    return TimeZoneInfo.Utc;
  }
}
