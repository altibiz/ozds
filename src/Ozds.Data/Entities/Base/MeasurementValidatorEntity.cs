using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("measurement_validators")]
public abstract class MeasurementValidatorEntity : AuditableEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}
