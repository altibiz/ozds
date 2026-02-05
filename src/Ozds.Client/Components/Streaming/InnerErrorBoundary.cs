using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Ozds.Client.Components.Streaming;

public class InnerErrorBoundary : ErrorBoundary
{
  [Parameter]
  public OzdsErrorBoundary This { get; set; } = default!;

  protected override Task OnErrorAsync(Exception exception)
  {
    This.OnError(exception);
    return Task.CompletedTask;
  }
}
