using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum AuditModel
{
  Query,
  Creation,
  Modification,
  Deletion,
  Restoration,
  Forgetting
}

public static class AuditModelExtensions
{
  public static AuditModel ToModel(this AuditEntity auditEntity)
  {
    return auditEntity switch
    {
      AuditEntity.Query => AuditModel.Query,
      AuditEntity.Creation => AuditModel.Creation,
      AuditEntity.Modification => AuditModel.Modification,
      AuditEntity.Deletion => AuditModel.Deletion,
      AuditEntity.Restoration => AuditModel.Restoration,
      AuditEntity.Forgetting => AuditModel.Forgetting,
      _ => throw new ArgumentOutOfRangeException(
        nameof(auditEntity),
        auditEntity, null)
    };
  }

  public static AuditEntity ToEntity(this AuditModel auditModel)
  {
    return auditModel switch
    {
      AuditModel.Query => AuditEntity.Query,
      AuditModel.Creation => AuditEntity.Creation,
      AuditModel.Modification => AuditEntity.Modification,
      AuditModel.Deletion => AuditEntity.Deletion,
      AuditModel.Restoration => AuditEntity.Restoration,
      AuditModel.Forgetting => AuditEntity.Forgetting,
      _ => throw new ArgumentOutOfRangeException(
        nameof(auditModel), auditModel,
        null)
    };
  }

  public static string ToTitle(this AuditModel audit)
  {
    return audit switch
    {
      AuditModel.Query => "Query",
      AuditModel.Creation => "Creation",
      AuditModel.Modification => "Modification",
      AuditModel.Deletion => "Deletion",
      AuditModel.Restoration => "Restoration",
      AuditModel.Forgetting => "Forgetting",
      _ => throw new ArgumentOutOfRangeException(
        nameof(audit), audit,
        null)
    };
  }
}
