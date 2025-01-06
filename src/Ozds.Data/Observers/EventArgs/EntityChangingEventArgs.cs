using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Observers.EventArgs;

public class EntitiesChangingEventArgs : System.EventArgs
{
  public required IReadOnlyList<EntityChangingEntry> Entities { get; init; }
}

public sealed record EntityChangingEntry(
  EntityChangingState State,
  IEntity Entity
);

public enum EntityChangingState
{
  Adding,
  Modifying,
  Removing
}
