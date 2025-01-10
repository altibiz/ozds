using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class LocalizationDrawer : OzdsComponentBase
{
  private static readonly CultureInfo _croatianCulture =
    new CultureInfo("hr-HR");

  private static readonly CultureInfo _englishCulture =
    new CultureInfo("en-US");

  [CascadingParameter]
  private LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  private CultureState CultureState { get; set; } = default!;

  private void SetLocalizationDrawerOpen(bool open)
  {
    LayoutState.SetLocalizationDrawerOpen(open);
  }

  private void OnCroatianClick()
  {
    CultureState.SetCulture(_croatianCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }

  private void OnEnglishClick()
  {
    CultureState.SetCulture(_englishCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }
}
