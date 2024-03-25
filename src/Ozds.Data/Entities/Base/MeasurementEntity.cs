using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp))]
public abstract class MeasurementEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }
}

[PrimaryKey(nameof(Timestamp), nameof(MeterId))]
public abstract class MeasurementEntity<T> : MeasurementEntity
  where T : MeterEntity
{
  [ForeignKey(nameof(Meter))] public string MeterId { get; set; } = default!;

  [Required] public virtual T Meter { get; set; } = default!;
}
