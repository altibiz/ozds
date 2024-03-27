using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

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

[TimescaleHypertable(nameof(Timestamp), nameof(AuditableEntityId), "number_partitions => 10")]
public class AuditEventEntity : EventEntity
{
  [ForeignKey(nameof(AuditableEntity))]
  public string AuditableEntityId { get; set; } = default!;

  public virtual AuditableEntity AuditableEntity { get; set; } = default!;

  public AuditEntity Audit { get; set; } = default!;
}

public class EventEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<EventEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("events")
      .HasDiscriminator<string>("kind");
  }
}
