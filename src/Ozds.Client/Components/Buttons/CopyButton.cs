using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.Services;

namespace Ozds.Client.Components.Buttons;

public partial class CopyButton : OzdsComponentBase
{

  [Parameter]
  public string? Text { get; set; }

  [Parameter]
  public Size Size { get; set; } = Size.Medium;

  [Parameter]
  public EventCallback OnCopied { get; set; }

  [Parameter]
  public EventCallback<Exception> OnCopyFailed { get; set; }

  [Inject]
  private ClipboardService ClipboardService { get; set; } = default!;

  [Inject]
  private ISnackbar Snackbar { get; set; } = default!;

  private async Task Copy()
  {
    if (Text is null)
    {
      return;
    }

    try
    {
      await ClipboardService.WriteTextAsync(Text);
      Snackbar.Add("Copied to clipboard", Severity.Success);
      await OnCopied.InvokeAsync();
    }
    catch (Exception ex)
    {
      Snackbar.Add("Copy failed", Severity.Success);
      await OnCopyFailed.InvokeAsync(ex);
    }
  }
}
