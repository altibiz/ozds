using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Streaming;

public class InnerErrorBoundary
  : Microsoft.AspNetCore.Components.Web.ErrorBoundary
{
  [Parameter]
  public ErrorBoundary This { get; set; } = default!;

  protected override Task OnErrorAsync(Exception exception)
  {
    This.OnError(exception);
    return Task.CompletedTask;
  }
}
