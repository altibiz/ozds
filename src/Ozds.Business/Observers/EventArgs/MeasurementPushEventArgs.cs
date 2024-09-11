using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementPushEventArgs(IReadOnlyList<IMeasurement> measurements)
  : System.EventArgs
{
  public IReadOnlyList<IMeasurement> Measurements { get; } = measurements;
}
