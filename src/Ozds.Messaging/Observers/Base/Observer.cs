using Ozds.Messaging.Observers.Abstractions;

namespace Ozds.Messaging.Observers.Base;

public abstract class Observer<TEventArgs> :
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
