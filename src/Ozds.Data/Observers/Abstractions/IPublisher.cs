namespace Ozds.Data.Observers.Abstractions;

public interface IPublisher
{
}

public interface IPublisher<TEventArgs> : IPublisher
  where TEventArgs : System.EventArgs
{
  public void Publish(TEventArgs eventArgs);
}

#pragma warning disable S2326 // Unused type parameters should be removed
public interface IPublisher<TSubscriber, TEventArgs> : IPublisher<TEventArgs>
#pragma warning restore S2326 // Unused type parameters should be removed
  where TSubscriber : ISubscriber<TEventArgs>
  where TEventArgs : System.EventArgs
{
}
