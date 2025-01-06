using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementFlushEventArgs : System.EventArgs
{
  public required IReadOnlyList<IMeasurement> Measurements { get; init; }
}
