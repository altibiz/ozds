using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

public abstract class MeasurementEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Required]
  public string Source { get; set; } = default!;

  [Required]
  [Column(TypeName = "timestamptz")]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }
}
