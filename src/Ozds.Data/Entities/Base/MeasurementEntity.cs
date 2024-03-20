using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;

namespace Ozds.Data.Entities.Base;

[PrimaryKey(nameof(Timestamp), nameof(Source))]
public abstract class MeasurementEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public string Source { get; set; } = default!;

  [Required]
  [TimescaleHypertable]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }
}
