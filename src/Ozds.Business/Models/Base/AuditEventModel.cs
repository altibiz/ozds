using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public class AuditEventModel : EventModel, IAuditEvent
{
  public required AuditModel Audit { get; set; }
}
