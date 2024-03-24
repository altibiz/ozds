using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeAuditEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  AuditModel Audit,
  string RepresentativeId
) : AuditEventModel(
  Id: Id,
  Timestamp: Timestamp,
  Level: Level,
  Audit: Audit,
  Description: Description
)
{
  public override object ToDbEntity()
  {
    return this.ToEntity();
  }
};

public static class RepresentativeAuditEventModelExtensions
{
  public static RepresentativeAuditEventModel ToModel(this RepresentativeAuditEventEntity entity) =>
    new(
      Id: entity.Id,
      Timestamp: entity.Timestamp,
      Level: entity.Level.ToModel(),
      Description: entity.Description,
      Audit: entity.Audit.ToModel(),
      RepresentativeId: entity.RepresentativeId
    );

  public static RepresentativeAuditEventEntity ToEntity(this RepresentativeAuditEventModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      Audit = model.Audit.ToEntity(),
      RepresentativeId = model.RepresentativeId
    };
}
