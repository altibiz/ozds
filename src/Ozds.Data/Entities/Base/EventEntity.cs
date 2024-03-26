using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[Table("events")]
[TimescaleHypertable(nameof(Timestamp))]
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

public class EventInheritedEntityTypeConfiguration : InheritedEntityTypeConfiguration<EventEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.ToTable("events").HasDiscriminator<int>("kind");
  }
}
