using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class LocationMeasurementLocationEntity : MeasurementLocationEntity
{
  public virtual LocationEntity Location { get; set; } = default!;
}
