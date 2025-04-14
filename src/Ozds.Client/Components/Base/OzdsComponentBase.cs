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

public abstract class OzdsComponentBase : ComponentBase, IDisposable
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
  {
    WriteIndented = true
  };

  private static readonly CancellationTokenSource _cancelledTokenSource =
    CreateCancelledTokenSource();

  private CancellationTokenSource? cancellationTokenSource = new();

  [CascadingParameter]
  private CultureState? CultureState { get; set; } = default!;

  [CascadingParameter]
  private ScopeState ScopeState { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  [Inject]
  private TemplateBinderFactory TemplateBinderFactory { get; set; } = default!;

  protected string Href => new Uri(NavigationManager.Uri).AbsolutePath;

  protected string LoginHref =>
    $"/login?returnUrl={Uri.EscapeDataString(Href)}";

  protected string LogoutHref =>
    $"/users/logoff?returnUrl=/login?returnUrl={Uri.EscapeDataString(Href)}";

  protected string IndexHref => BasedHref("/");

  protected IServiceProvider ScopedServices => ScopeState.ScopedServices;

  protected CancellationToken CancellationToken =>
    cancellationTokenSource?.Token ?? _cancelledTokenSource.Token;

  protected bool IsDisposed { get; private set; }

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
    var key = localizationQueries.Key(type);
    var culture = GetCulture();
    return localizationQueries.Translate(culture, key);
  }

  protected string Translate(Type type, string member)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var key = localizationQueries.Key(type, member);
    var culture = GetCulture();
    return localizationQueries.Translate(culture, key);
  }

  protected string Translate(MemberExpression member)
  {
    var localizationQueries = ScopedServices
      .GetRequiredService<LocalizationQueries>();
    var key = localizationQueries.Key(member);
    var culture = GetCulture();
    return localizationQueries.Translate(culture, key);
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
    return CultureState?.Culture ?? CroatianCulture;
  }

  protected Task SetCulture(CultureInfo culture)
  {
    if (CultureState is { } cultureState)
    {
      return cultureState.SetCulture(culture);
    }

    return Task.CompletedTask;
  }

  private static CancellationTokenSource CreateCancelledTokenSource()
  {
    var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.Cancel();
    return cancellationTokenSource;
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
#pragma warning disable S1066 // Mergeable "if" statements should be combined
      if (cancellationTokenSource is not null)
#pragma warning restore S1066 // Mergeable "if" statements should be combined
      {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;
      }
    }

    IsDisposed = true;
  }
}
