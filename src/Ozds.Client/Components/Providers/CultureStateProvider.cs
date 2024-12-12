using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class CultureStateProvider : ComponentBase
{
  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  [Parameter]
  public CultureInfo? Culture { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private ILocalStorageService LocalStorageService { get; set; } = default!;

  private const string CultureKey = "culture";

  private CultureState? _state;

  protected override async Task OnParametersSetAsync()
  {
    var culture = Culture;

    if (culture is null)
    {
      culture = GetCultureFromUri();
    }

    if (culture is { })
    {
      await SetCultureToLocalStorage(culture);
    }

    if (culture is null)
    {
      culture = await GetCultureFromLocalStorage();
    }

    if (culture is null)
    {
      culture = new CultureInfo("hr");
      await SetCultureToLocalStorage(culture);
    }

    _state = new CultureState(
      culture,
      (culture) => InvokeAsync(async () =>
      {
        await SetCultureToLocalStorage(culture);

        var uriCulture = GetCultureFromUri();
        if (uriCulture is { })
        {
          SetCultureToUri(culture);
          return;
        }

        _state = _state! with { Culture = culture };
        StateHasChanged();
      })
    );
  }

  private void SetCultureToUri(CultureInfo culture)
  {
    if (GetCultureFromUri() is { } uriCulture)
    {
      if (uriCulture.TwoLetterISOLanguageName
        == culture.TwoLetterISOLanguageName)
      {
        return;
      }

      var uri = new Uri(NavigationManager.Uri);
      var segments = uri.Segments;
      segments[2] = $"{culture.TwoLetterISOLanguageName}/";
      var path = string.Join("", segments);
      NavigationManager.NavigateTo(path, forceLoad: true);
    }
    else
    {
      var uri = new Uri(NavigationManager.Uri);
      var segments = uri.Segments;
      var newSegments = segments
        .Take(1)
        .Append($"{culture.TwoLetterISOLanguageName}/")
        .Concat(segments.Skip(1));
      var path = string.Join("", newSegments);
      NavigationManager.NavigateTo(path, forceLoad: true);
    }
  }

  private async Task SetCultureToLocalStorage(CultureInfo culture)
  {
    await LocalStorageService
      .SetItemAsync(CultureKey, culture.TwoLetterISOLanguageName);
  }

  private CultureInfo? GetCultureFromUri()
  {
    var uri = new Uri(NavigationManager.Uri);
    var segments = uri.Segments;
    var cultureString = segments.ElementAtOrDefault(2)
      ?.TrimStart('/')
      .TrimEnd('/');
    return ParseCultureString(cultureString);
  }

  private async Task<CultureInfo?> GetCultureFromLocalStorage()
  {
    return await LocalStorageService.GetItemAsync<string>(CultureKey)
      is { } cultureString
        ? ParseCultureString(cultureString)
        : default;
  }

  private static CultureInfo? ParseCultureString(string? cultureString)
  {
    if (cultureString is null)
    {
      return default;
    }

    CultureInfo? culture;
    try
    {
      culture = new CultureInfo(cultureString);
    }
    catch (Exception)
    {
      return default;
    }

    return culture;
  }
}
