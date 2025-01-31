using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Time;
using Ozds.Client.Extensions;

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

  private IServiceScope? scope;

  [Inject]
  private ILocalizer Localizer { get; set; } = default!;

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
      return $"/users/logoff?returnUrl=/login?returnUrl={
        Uri.EscapeDataString(Href)
      }";
    }
  }

  protected string IndexHref
  {
    get { return BasedHref("/"); }
  }

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  [Inject]
  private TemplateBinderFactory TemplateBinderFactory { get; set; } = default!;

  protected IServiceProvider ScopedServices
  {
    get
    {
      if (ScopeFactory == null)
      {
        throw new InvalidOperationException(
          "Services cannot be accessed before the component is initialized."
        );
      }

      ObjectDisposedException.ThrowIf(IsDisposed, this);
      scope ??= ScopeFactory.CreateScope();

      return scope.ServiceProvider;
    }
  }

  [Inject]
  private IServiceScopeFactory ScopeFactory { get; set; } = default!;

  protected CancellationToken CancellationToken
  {
    get
    {
      return cancellationTokenSource?.Token ?? _cancelledTokenSource.Token;
    }
  }

  protected bool IsDisposed { get; private set; }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
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

  protected string Translate(string notLocalized)
  {
    var localized = Localizer.Translate(notLocalized);
    return localized;
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

  private static CancellationTokenSource CreateCancelledTokenSource()
  {
    var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.Cancel();
    return cancellationTokenSource;
  }

  protected virtual void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
      if (cancellationTokenSource is not null)
      {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;
      }

      if (scope is not null)
      {
        scope.Dispose();
      }
    }

    IsDisposed = true;
  }
}
