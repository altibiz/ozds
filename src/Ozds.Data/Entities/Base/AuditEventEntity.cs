using Ozds.Data.Entities.Enums;

// TODO: extension method for querying auditable entities from these events

namespace Ozds.Data.Entities.Base;

public class AuditEventEntity : EventEntity
{
  public string AuditableEntityId { get; set; } = default!;

  public string AuditableEntityType { get; set; } = default!;

  public string AuditableEntityTable { get; set; } = default!;

  public AuditEntity Audit { get; set; } = default!;
}
