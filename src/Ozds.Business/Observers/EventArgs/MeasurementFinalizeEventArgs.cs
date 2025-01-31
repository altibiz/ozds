using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementFinalizeEventArgs : System.EventArgs
{
  public required IReadOnlyList<IMeasurement> Measurements { get; init; }

  public required IReadOnlyList<IAggregate> Aggregates { get; init; }
}
