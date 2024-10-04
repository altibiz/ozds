using Ozds.Business.Observers.Abstractions;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Observers;

public class ErrorObserver : IErrorPublisher, IErrorSubscriber
{
  public void PublishError(ErrorEventArgs eventArgs)
  {
    OnError?.Invoke(this, eventArgs);
  }

  public void SubscribeError(EventHandler<ErrorEventArgs> handler)
  {
    OnError += handler;
  }

  public void UnsubscribeError(EventHandler<ErrorEventArgs> handler)
  {
    OnError -= handler;
  }

  private event EventHandler<ErrorEventArgs>? OnError;
}
