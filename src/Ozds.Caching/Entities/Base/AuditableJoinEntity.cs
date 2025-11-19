using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Base;

public abstract class AuditableJoinEntity : JoinEntity, IAuditableEntity
{
  public DateTimeOffset CreatedOn { get; set; }

  public required string? CreatedById { get; set; }
}
