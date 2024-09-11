namespace Ozds.Iot.Observers.Abstractions;

public interface IPublisher
{
}

#pragma warning disable S2326 // Unused type parameters should be removed
public interface IPublisher<TSubscriber> : IPublisher
#pragma warning restore S2326 // Unused type parameters should be removed
  where TSubscriber : class, ISubscriber
{
}
