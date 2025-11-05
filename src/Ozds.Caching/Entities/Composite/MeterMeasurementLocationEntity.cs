using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Base;

namespace Ozds.Caching.Entities.Composite;

public class MeterMeasurementLocationEntity : ICompositeEntity
{
  public string Id
  {
    get => Meter?.Id ?? string.Empty;
    set
    {
      if (Meter is not null)
      {
        Meter.Id = value;
      }
    }
  }

  public string Title { get => Meter.Title; }

  public MeterEntity Meter { get; set; } = default!;

  public MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;
}
