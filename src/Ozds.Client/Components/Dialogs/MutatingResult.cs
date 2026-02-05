using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Dialogs;

public enum MutatingResultNavigationBehavior
{
  Reload,
  GoBack,
  Logout,
}

public partial class MutatingResult : OzdsComponentBase
{
  [CascadingParameter]
  public IMudDialogInstance MudDialog { get; set; } = default!;

  [CascadingParameter]
  public UserState UserState { get; set; } = default!;

  [Parameter]
  public RenderFragment? Body { get; set; }

  [Parameter]
  public MutatingResultNavigationBehavior? NavigationBehavior { get; set; } =
    MutatingResultNavigationBehavior.GoBack;

  [Parameter]
  public Action<IMudDialogInstance>? Exit { get; set; }

  [Parameter]
  public Func<IMudDialogInstance, Task>? ExitAsync { get; set; }

  private async Task OnExit()
  {
    if (Exit is { } exit)
    {
      exit(MudDialog);
    }

    if (ExitAsync is { } exitAsync)
    {
      await exitAsync(MudDialog);
    }

    if (NavigationBehavior == MutatingResultNavigationBehavior.Reload)
    {
      NavigateHere();
    }

    if (NavigationBehavior == MutatingResultNavigationBehavior.GoBack)
    {
      NavigateBack();
    }

    MudDialog.Close(DialogResult.Ok(true));
  }
}
