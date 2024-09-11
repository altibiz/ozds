using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntityChangesSubscriber : ISubscriber<IEntityChangesPublisher>
{
  public void SubscribeEntitiesChanging(
    EventHandler<EntitiesChangingEventArgs> handler);

  public void UnsubscribeEntitiesChanging(
    EventHandler<EntitiesChangingEventArgs> handler);

  public void SubscribeEntitiesChanged(
    EventHandler<EntitiesChangedEventArgs> handler);

  public void UnsubscribeEntitiesChanged(
    EventHandler<EntitiesChangedEventArgs> handler);
}
