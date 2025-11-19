using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

// NOTE: to be used inside ErrorBoundary and ScopeStateProvider
public partial class CultureStateProvider : DisposableComponentBase
{
  private const string CultureKey = "culture";

  private CultureState? _state;

  private LocalizationQueries? localizationQueries;

  [CascadingParameter]
  private ScopeState ScopeState { get; set; } = default!;

  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  [Parameter]
  public string? CultureId { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private ILocalStorageService LocalStorageService { get; set; } = default!;

  protected IServiceProvider ScopedServices
  {
    get
    {
      return ScopeState?.ScopedServices ??
        throw new InvalidOperationException($"{this} got disposed");
    }
  }

  private LocalizationQueries LocalizationQueries
  {
    get
    {
      return localizationQueries ??= ScopedServices
        .GetRequiredService<LocalizationQueries>();
    }
  }

  protected override async Task OnParametersSetAsync()
  {
    var culture = CultureId is null
      ? null
      : LocalizationQueries.IdToCulture(CultureId);

    culture ??= GetCultureFromUri();

    if (culture is not null)
    {
      await SetCultureToLocalStorage(culture);
    }

    culture ??= await GetCultureFromLocalStorage();

    if (culture is null)
    {
      culture = LocalizationQueries.DefaultCulture;
      await SetCultureToLocalStorage(culture);
    }

    _state = new CultureState(
      culture,
      async culture =>
      {
        await SetCultureToLocalStorage(culture);

        var uriCulture = GetCultureFromUri();
        if (uriCulture is not null)
        {
          SetCultureToUri(culture);
          return;
        }

        _state = _state! with { Culture = culture };

        await InvokeAsync(StateHasChanged);
      }
    );
  }

  private void SetCultureToUri(CultureInfo culture)
  {
    if (GetCultureFromUri() is { } uriCulture)
    {
      if (LocalizationQueries.CultureToId(uriCulture)
        == LocalizationQueries.CultureToId(culture))
      {
        return;
      }

      var uri = new Uri(NavigationManager.Uri);
      var segments = uri.Segments;
      segments[2] = $"{LocalizationQueries.CultureToId(culture)}/";
      var path = string.Join("", segments);
      NavigationManager.NavigateTo(path, true);
    }
    else
    {
      var uri = new Uri(NavigationManager.Uri);
      var segments = uri.Segments;
      var newSegments = segments
        .Take(1)
        .Append($"{LocalizationQueries.CultureToId(culture)}/")
        .Concat(segments.Skip(1));
      var path = string.Join("", newSegments);
      NavigationManager.NavigateTo(path, true);
    }
  }

  private async Task SetCultureToLocalStorage(CultureInfo culture)
  {
    await LocalStorageService
      .SetItemAsync(
        CultureKey,
        LocalizationQueries.CultureToId(culture),
        CancellationToken);
  }

  private CultureInfo? GetCultureFromUri()
  {
    var uri = new Uri(NavigationManager.Uri);
    var segments = uri.Segments;
    var cultureString = segments.ElementAtOrDefault(2)
      ?.TrimStart('/')
      .TrimEnd('/');
    return cultureString is null
      ? null
      : LocalizationQueries.IdToCulture(cultureString);
  }

  private async Task<CultureInfo?> GetCultureFromLocalStorage()
  {
    return await LocalStorageService.GetItemAsync<string>(
        CultureKey,
        CancellationToken)
      is { } cultureString
      ? LocalizationQueries.IdToCulture(cultureString)
      : default;
  }
}
