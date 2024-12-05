using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class CultureStateProvider : ComponentBase
{
  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  [Parameter]
  public CultureInfo Culture { get; set; } = default!;

  private CultureState? _state;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  protected override void OnParametersSet()
  {
    _state = new CultureState(
      Culture,
      (culture) =>
      {
        var uri = new Uri(NavigationManager.Uri);
        var segments = uri.Segments;
        segments[2] = $"{culture.TwoLetterISOLanguageName}/";
        var path = string.Join("", segments);

        NavigationManager.NavigateTo(path, forceLoad: true);
      }
    );
  }
}
