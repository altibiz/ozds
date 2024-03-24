using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Base;

public class AuditEventEntity : EventEntity
{
  [ForeignKey(nameof(AuditableEntity))]
  public string AuditableEntityId { get; set; } = default!;

  public virtual AuditableEntity AuditableEntity { get; set; } = default!;

  public AuditEntity Audit { get; set; } = default!;
}
