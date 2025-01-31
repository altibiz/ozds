using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public partial class MutatingResult : OzdsComponentBase
{
  [CascadingParameter]
  public IMudDialogInstance MudDialog { get; set; } = default!;

  [Parameter]
  public string? Body { get; set; }

  private void OnExit()
  {
    NavigateBack();
    MudDialog.Close(DialogResult.Ok(true));
  }
}
