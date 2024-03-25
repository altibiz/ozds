using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("measurement_locations")]
public class MeasurementLocationEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}
