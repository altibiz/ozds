using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ozds.Client.Components.Dialogs;

public partial class MutateSuccess
{
  [CascadingParameter]
  public IMudDialogInstance MudDialog { get; set; } = default!;

  private void Submit()
  {
    MudDialog.Close(DialogResult.Ok(true));
  }
}
