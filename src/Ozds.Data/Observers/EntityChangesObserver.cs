using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Observers.Abstractions;

namespace Ozds.Data.Observers;

public class EntityChangesObserver
  : IEntityChangesPublisher, IEntityChangesSubscriber
{
  public event EventHandler<EntityAddedEventArgs>? OnEntityAdded;

  public event EventHandler<EntityModifiedEventArgs>? OnEntityModified;

  public event EventHandler<EntityRemovedEventArgs>? OnEntityRemoved;

  public void PublishEntityAdded(IEntity entity)
  {
    OnEntityAdded?.Invoke(this, new EntityAddedEventArgs { Entity = entity });
  }

  public void PublishEntityModified(IEntity entity)
  {
    OnEntityModified?.Invoke(this, new EntityModifiedEventArgs { Entity = entity });
  }

  public void PublishEntityRemoved(IEntity entity)
  {
    OnEntityRemoved?.Invoke(this, new EntityRemovedEventArgs { Entity = entity });
  }
}
