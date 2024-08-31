using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Observers.Abstractions;

public class EntityAddedEventArgs : EventArgs
{
  public required IEntity Entity { get; init; }
}

public class EntityRemovedEventArgs : EventArgs
{
  public required IEntity Entity { get; init; }
}

public class EntityModifiedEventArgs : EventArgs
{
  public required IEntity Entity { get; init; }
}

public interface IEntityChangesSubscriber : ISubscriber<IEntityChangesPublisher>
{
  public event EventHandler<EntityAddedEventArgs>? OnEntityAdded;

  public event EventHandler<EntityModifiedEventArgs>? OnEntityModified;

  public event EventHandler<EntityRemovedEventArgs>? OnEntityRemoved;
}
