using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities;

public abstract class MeasurementEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [HypertableColumn]
  public string Source { get; set; } = default!;

  [HypertableColumn]
  [Column(TypeName = "timestamptz")]
  public DateTimeOffset Timestamp
  {
    get => _timestamp.ToUniversalTime();
    set => _timestamp = value.ToUniversalTime();
  }
}
