using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RepresentativeAuditEventModel : AuditEventModel
{
  [Required]
  public required string RepresentativeId { get; set; }
}

public static class RepresentativeAuditEventModelExtensions
{
  public static RepresentativeAuditEventModel ToModel(
    this RepresentativeAuditEventEntity entity)
  {
    return new RepresentativeAuditEventModel()
    {
      Id = entity.Id,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description,
      Audit = entity.Audit.ToModel(),
      RepresentativeId = entity.RepresentativeId
    };
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
