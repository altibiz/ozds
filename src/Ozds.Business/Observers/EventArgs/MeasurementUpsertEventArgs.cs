using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementFinalizeEventArgs : System.EventArgs
{
  public IReadOnlyList<IMeasurement> Measurements { get; set; } = default!;

  public IReadOnlyList<IAggregate> Aggregates { get; set; } = default!;
}
