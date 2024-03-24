using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public abstract record AuditEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  AuditModel Audit
) : EventModel(
  Id: Id,
  Timestamp: Timestamp,
  Level: Level,
  Description: Description
), IAuditEvent;
