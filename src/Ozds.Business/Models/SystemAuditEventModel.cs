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
  Id,
  Timestamp,
  Level,
  Description,
  Audit
);

public static class SystemAuditEventModelExtensions
{
  public static SystemAuditEventModel ToModel(
    this SystemAuditEventEntity entity)
  {
    return new SystemAuditEventModel(
      entity.Id,
      entity.Timestamp,
      entity.Level.ToModel(),
      entity.Description,
      entity.Audit.ToModel()
    );
  }

  public static SystemAuditEventEntity ToEntity(
    this SystemAuditEventModel model)
  {
    return new SystemAuditEventEntity
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      Audit = model.Audit.ToEntity()
    };
  }
}
