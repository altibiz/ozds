using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class LocalizationDrawer : OzdsComponentBase
{
  private static readonly CultureInfo _croatianCulture = new("hr-HR");

  private static readonly CultureInfo _englishCulture = new("en-US");

  [CascadingParameter]
  private LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  private CultureState CultureState { get; set; } = default!;

  private void SetLocalizationDrawerOpen(bool open)
  {
    LayoutState.SetLocalizationDrawerOpen(open);
  }

  private async Task OnCroatianClick()
  {
    await CultureState.SetCulture(_croatianCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }

  private async Task OnEnglishClick()
  {
    await CultureState.SetCulture(_englishCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }
}
