using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp))]
[PrimaryKey(nameof(Timestamp), nameof(Meter))]
public abstract class MeasurementEntity<T> : ReadonlyEntity where T : MeterEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [ForeignKey(nameof(Meter))]
  public string MeterId { get; set; } = default!;

  [Required]
  public virtual T Meter { get; set; } = default!;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }
}
