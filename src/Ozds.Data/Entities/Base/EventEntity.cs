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
public class EventEntity : ReadonlyEntity
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

public class AuditEventEntity : EventEntity
{
  [ForeignKey(nameof(AuditableEntity))]
  public string AuditableEntityId { get; set; } = default!;

  public virtual AuditableEntity AuditableEntity { get; set; } = default!;

  public AuditEntity Audit { get; set; } = default!;
}

public class EventEntityTypeConfiguration : EntityTypeConfiguration<EventEntity>
{
  public override void Configure(EntityTypeBuilder<EventEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .HasDiscriminator<string>("kind");
  }
}
