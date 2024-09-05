using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class SystemAuditEventModelEntityConverter : ModelEntityConverter<
  SystemAuditEventModel, SystemAuditEventEntity>
{
  protected override SystemAuditEventEntity ToEntity(
    SystemAuditEventModel model)
  {
    return model.ToEntity();
  }

  protected override SystemAuditEventModel ToModel(
    SystemAuditEventEntity entity)
  {
    return entity.ToModel();
  }
}

public static class SystemAuditEventModelEntityExtensions
{
  public static SystemAuditEventModel ToModel(
    this SystemAuditEventEntity entity)
  {
    return new SystemAuditEventModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Content = entity.Content,
      Audit = entity.Audit.ToModel(),
      Categories = entity.Categories.Select(c => c.ToModel()).ToList()
    };
  }

  public static SystemAuditEventEntity ToEntity(
    this SystemAuditEventModel model)
  {
    return new SystemAuditEventEntity
    {
      Id = model.Id,
      Title = model.Title,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Content = model.Content,
      Audit = model.Audit.ToEntity(),
      Categories = model.Categories.Select(c => c.ToEntity()).ToList()
    };
  }
}
