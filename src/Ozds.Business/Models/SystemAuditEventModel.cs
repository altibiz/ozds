using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record SystemAuditEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  AuditModel Audit
) : AuditEventModel(
  Id: Id,
  Timestamp: Timestamp,
  Level: Level,
  Description: Description,
  Audit: Audit
);

public static class SystemAuditEventModelExtensions
{
  public static SystemAuditEventModel ToModel(this SystemAuditEventEntity entity) =>
    new(
      Id: entity.Id,
      Timestamp: entity.Timestamp,
      Level: entity.Level.ToModel(),
      Description: entity.Description,
      Audit: entity.Audit.ToModel()
    );

  public static SystemAuditEventEntity ToEntity(this SystemAuditEventModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      Audit = model.Audit.ToEntity()
    };
}
