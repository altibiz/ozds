using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class DataModelsChangedEventArgs : System.EventArgs
{
  public required IReadOnlyList<DataModelChangedEntry> Models { get; init; }
}

public sealed record DataModelChangedEntry(
  DataModelChangedState State,
  IModel Model
);

public enum DataModelChangedState
{
  Added,
  Modified,
  Removed
}
