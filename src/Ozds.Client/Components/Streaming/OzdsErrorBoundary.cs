using Microsoft.AspNetCore.Components;
using Ozds.Business.Observers.Abstractions;
using Ozds.Client.Components.Base;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Client.Components.Streaming;

// NOTE: rendered at root so keep dependencies minimal
public partial class OzdsErrorBoundary : DisposableComponentBase
{
  private InnerErrorBoundary? inner;

  [Parameter]
  public RenderFragment? ChildContent { get; set; } = default!;

  [Inject]
  private IErrorPublisher ErrorPublisher { get; set; } = default!;

  private void OnRecover()
  {
    inner?.Recover();
  }

  public void OnError(Exception exception)
  {
    var eventArgs = new ErrorEventArgs
    {
      Message = "UI Error",
      Exception = exception
    };
    ErrorPublisher.Publish(eventArgs);
  }
}
