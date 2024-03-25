using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Base;

[TimescaleHypertable(nameof(Timestamp))]
[Table("events")]
public abstract class EventEntity : ReadonlyEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Key] public string Id { get; set; } = default!;

  [Required]
  public DateTimeOffset Timestamp
  {
    get { return _timestamp.ToUniversalTime(); }
    set { _timestamp = value.ToUniversalTime(); }
  }

  [Required] public LevelEntity Level { get; set; } = default!;

  public string Description { get; set; } = default!;
}
