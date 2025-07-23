using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class DataModelsChangingEventArgs : System.EventArgs
{
  public required IReadOnlyList<DataModelChangingEntry> Models { get; init; }
}

public sealed record DataModelChangingEntry(
  DataModelChangingState State,
  IModel Model
);

public enum DataModelChangingState
{
  Adding,
  Modifying,
  Removing
}
