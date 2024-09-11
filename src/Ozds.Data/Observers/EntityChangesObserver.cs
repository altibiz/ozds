using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers;

public class EntityChangesObserver
  : IEntityChangesPublisher, IEntityChangesSubscriber
{
  public void PublishEntitiesChanged(EntitiesChangedEventArgs eventArgs)
  {
    OnEntitiesChanged?.Invoke(this, eventArgs);
  }

  public void PublishEntitiesChanging(EntitiesChangingEventArgs eventArgs)
  {
    OnEntitiesChanging?.Invoke(this, eventArgs);
  }

  public void SubscribeEntitiesChanging(
    EventHandler<EntitiesChangingEventArgs> handler)
  {
    OnEntitiesChanging += handler;
  }

  public void UnsubscribeEntitiesChanging(
    EventHandler<EntitiesChangingEventArgs> handler)
  {
    OnEntitiesChanging -= handler;
  }

  public void SubscribeEntitiesChanged(
    EventHandler<EntitiesChangedEventArgs> handler)
  {
    OnEntitiesChanged += handler;
  }

  public void UnsubscribeEntitiesChanged(
    EventHandler<EntitiesChangedEventArgs> handler)
  {
    OnEntitiesChanged -= handler;
  }

  private event EventHandler<EntitiesChangingEventArgs>? OnEntitiesChanging;

  private event EventHandler<EntitiesChangedEventArgs>? OnEntitiesChanged;
}
