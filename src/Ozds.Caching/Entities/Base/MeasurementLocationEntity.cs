using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Base;

public abstract class MeasurementLocationEntity :
  TrackableEntity,
  IMeasurementLocationEntity
{
  public string MeterId { get; set; } = default!;
}
