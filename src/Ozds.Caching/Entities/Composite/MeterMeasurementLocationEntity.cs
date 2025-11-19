using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Base;

namespace Ozds.Caching.Entities.Composite;

public class MeterMeasurementLocationEntity : ICompositeEntity
{
  public MeterEntity Meter { get; set; } = default!;

  public MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  public string Id
  {
    get { return Meter?.Id ?? string.Empty; }
    set
    {
      if (Meter is not null)
      {
        Meter.Id = value;
      }
    }
  }

  public string Title
  {
    get { return Meter.Title; }
  }
}
