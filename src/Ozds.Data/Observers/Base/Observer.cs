using Ozds.Data.Observers.Abstractions;

namespace Ozds.Data.Observers.Base;

public class Observer<TEventArgs> :
  IPublisher<TEventArgs>,
  ISubscriber<TEventArgs>
  where TEventArgs : System.EventArgs
{
  public void Publish(TEventArgs eventArgs)
  {
    Event?.Invoke(this, eventArgs);
  }

  public void Subscribe(EventHandler<TEventArgs> eventHandler)
  {
    Event += eventHandler;
  }

  public void Unsubscribe(EventHandler<TEventArgs> eventHandler)
  {
    Event -= eventHandler;
  }

  private event EventHandler<TEventArgs>? Event;
}
