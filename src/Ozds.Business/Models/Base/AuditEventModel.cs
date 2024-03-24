using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public record AuditEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  AuditModel Audit
) : EventModel(Id, Timestamp, Level, Description), IAuditEvent;
