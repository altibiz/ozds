using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities;

public abstract class HypertableEntity
{
  [NotMapped] private DateTimeOffset _timestamp;

  [Column(TypeName = "text")] public string Source { get; set; } = default!;

  [HypertableColumn]
  [Column(TypeName = "timestamptz")]
  public DateTimeOffset Timestamp
  {
    get => _timestamp.ToUniversalTime();
    set => _timestamp = value.ToUniversalTime();
  }

  [Column(TypeName = "bigint")]
  public long Milliseconds
  {
    get => Timestamp.Ticks / TimeSpan.TicksPerMillisecond;
    // NOTE: https://stackoverflow.com/a/52367289
    private set { }
  }
}
