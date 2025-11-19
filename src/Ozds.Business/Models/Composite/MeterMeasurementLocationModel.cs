using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class MeterMeasurementLocationModel : ICachedComposite
{
  public MeterModel Meter { get; set; } = default!;

  public MeasurementLocationModel MeasurementLocation { get; set; } =
    default!;

  public string CacheId
  {
    get { return Meter.Id; }
  }
}
