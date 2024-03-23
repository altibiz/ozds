using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class RepresentativeAuditEventEntity : RepresentativeEventEntity
{
  public AuditEntity Audit { get; set; }
}
