using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Streaming;

public partial class MutatingResult : OzdsComponentBase
{
  [CascadingParameter]
  public MudDialogInstance MudDialog { get; set; } = default!;

  [Parameter]
  public string? Body { get; set; } = default!;

  private void OnExit()
  {
    NavigateBack();
    MudDialog.Close(DialogResult.Ok(true));
  }
}
