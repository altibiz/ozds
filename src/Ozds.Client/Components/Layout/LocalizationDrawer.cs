using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class LocalizationDrawer : OzdsComponentBase
{
  private LocalizationQueries? localizationQueries;

  [CascadingParameter]
  private LayoutState LayoutState { get; set; } = default!;

  private LocalizationQueries LocalizationQueries
  {
    get
    {
      return localizationQueries ??= ScopedServices
        .GetRequiredService<LocalizationQueries>();
    }
  }

  private void SetLocalizationDrawerOpen(bool open)
  {
    LayoutState.SetLocalizationDrawerOpen(open);
  }

  private async Task OnCroatianClick()
  {
    await SetCulture(CroatianCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }

  private async Task OnEnglishClick()
  {
    await SetCulture(EnglishCulture);
    LayoutState.SetLocalizationDrawerOpen(false);
  }
}
