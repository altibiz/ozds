namespace Ozds.Jobs.Observers.Abstractions;

public interface ISubscriber
{
}

#pragma warning disable S2326 // Unused type parameters should be removed
public interface ISubscriber<TPublisher> : ISubscriber
#pragma warning restore S2326 // Unused type parameters should be removed
  where TPublisher : IPublisher
{
}
