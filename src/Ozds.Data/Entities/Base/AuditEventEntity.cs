using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class AuditEventEntity : EventEntity, IAuditEventEntity
{
  public string AuditableEntityId { get; set; } = default!;

  public string AuditableEntityType { get; set; } = default!;

  public string AuditableEntityTable { get; set; } = default!;

  public AuditEntity Audit { get; set; } = default!;
}

public class AuditEventEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration<AuditEventEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.HasIndex(
      new[]
      {
        nameof(AuditEventEntity.Audit),
        nameof(AuditEventEntity.AuditableEntityType),
        nameof(AuditEventEntity.AuditableEntityId)
      },
      "ix_events_auditable_entity_type_auditable_entity_id"
    );

    builder.HasIndex(
      new[]
      {
        nameof(AuditEventEntity.Audit),
        nameof(AuditEventEntity.AuditableEntityTable),
        nameof(AuditEventEntity.AuditableEntityId)
      },
      "ix_events_auditable_entity_table_auditable_entity_id"
    );
  }
}
