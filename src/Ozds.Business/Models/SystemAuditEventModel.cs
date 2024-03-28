using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class SystemAuditEventModel : AuditEventModel { }

public static class SystemAuditEventModelExtensions
{
  public static SystemAuditEventModel ToModel(
    this SystemAuditEventEntity entity)
  {
    return new SystemAuditEventModel()
    {
      Id = entity.Id,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description,
      Audit = entity.Audit.ToModel()
    };
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
