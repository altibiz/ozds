using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementsBufferedEventArgs : System.EventArgs
{
  public required IReadOnlyList<IMeasurement> Measurements { get; init; }
}
