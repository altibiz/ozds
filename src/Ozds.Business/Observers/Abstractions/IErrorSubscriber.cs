using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IErrorSubscriber : ISubscriber<IErrorPublisher>
{
  public void SubscribeError(EventHandler<ErrorEventArgs> handler);

  public void UnsubscribeError(EventHandler<ErrorEventArgs> handler);
}
