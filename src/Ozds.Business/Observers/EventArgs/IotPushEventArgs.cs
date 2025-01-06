using Ozds.Business.Buffers;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class IotPushEventArgs : System.EventArgs
{
  public required string MessengerId { get; init; }

  public required MeasurementBufferBehavior BufferBehavior { get; init; }

  public required List<IMeasurement> Measurements { get; init; }
}
