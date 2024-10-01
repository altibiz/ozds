using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Observers.EventArgs;

public class EntitiesChangedEventArgs : System.EventArgs
{
  public required IReadOnlyList<EntityChangedEntry> Entities { get; init; }
}

public sealed record EntityChangedEntry(
  EntityChangedState State,
  IEntity Entity
);

public enum EntityChangedState
{
  Added,
  Modified,
  Removed
}
