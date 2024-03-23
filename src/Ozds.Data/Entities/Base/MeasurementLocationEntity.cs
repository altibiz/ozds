using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("measurement_locations")]
public class MeasurementLocationEntity : SoftDeletableEntity
{
  public virtual MeterEntity Meter { get; set; } = default!;
}
