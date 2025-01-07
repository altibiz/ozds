using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface IAuditEventEntity : IEventEntity
{
  public string AuditableEntityId { get; }

  public string AuditableEntityType { get; }

  public string AuditableEntityTable { get; }

  public AuditEntity Audit { get; }
}
