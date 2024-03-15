using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

public abstract class MeasurementEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  public string Source { get; set; } = default!;

  [Column(TypeName = "timestamptz")]
  public DateTimeOffset Timestamp
  {
    get => _timestamp.ToUniversalTime();
    set => _timestamp = value.ToUniversalTime();
  }
}
