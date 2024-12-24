using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class MeasurementFlushEventArgs : System.EventArgs
{
  public IReadOnlyList<IMeasurement> Measurements { get; set; } = default!;
}
