using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Base;

[PrimaryKey(nameof(Interval), nameof(Timestamp), nameof(Meter))]
public abstract class AggregateEntity<T> : MeasurementEntity<T>
  where T : MeterEntity
{
  [Required]
  public long Count { get; set; }

  [Required]
  public IntervalEntity Interval { get; set; }
}
