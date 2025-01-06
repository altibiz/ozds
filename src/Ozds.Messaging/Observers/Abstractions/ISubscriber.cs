namespace Ozds.Messaging.Observers.Abstractions;

public interface ISubscriber
{
}

public interface ISubscriber<TEventArgs> : ISubscriber
  where TEventArgs : System.EventArgs
{
  public void Subscribe(EventHandler<TEventArgs> eventHandler);

  public void Unsubscribe(EventHandler<TEventArgs> eventHandler);
}

#pragma warning disable S2326 // Unused type parameters should be removed
public interface ISubscriber<TPublisher, TEventArgs> : ISubscriber<TEventArgs>
#pragma warning restore S2326 // Unused type parameters should be removed
  where TPublisher : IPublisher<TEventArgs>
  where TEventArgs : System.EventArgs
{
}
