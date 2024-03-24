using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAuditEvent : IEvent
{
  public AuditModel Audit { get; }
}
