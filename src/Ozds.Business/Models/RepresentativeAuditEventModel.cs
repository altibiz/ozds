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
  Id,
  Timestamp,
  Level,
  Audit: Audit,
  Description: Description
);

public static class RepresentativeAuditEventModelExtensions
{
  public static RepresentativeAuditEventModel ToModel(
    this RepresentativeAuditEventEntity entity)
  {
    return new RepresentativeAuditEventModel(
      entity.Id,
      entity.Timestamp,
      entity.Level.ToModel(),
      entity.Description,
      entity.Audit.ToModel(),
      entity.RepresentativeId
    );
  }

  public static RepresentativeAuditEventEntity ToEntity(
    this RepresentativeAuditEventModel model)
  {
    return new RepresentativeAuditEventEntity
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      Audit = model.Audit.ToEntity(),
      RepresentativeId = model.RepresentativeId
    };
  }
}
