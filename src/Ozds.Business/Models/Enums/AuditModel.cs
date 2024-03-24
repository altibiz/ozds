using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum AuditModel
{
  Query,
  Creation,
  Modification,
  Deletion
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
      _ => throw new ArgumentOutOfRangeException(nameof(auditEntity), auditEntity, null)
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
      _ => throw new ArgumentOutOfRangeException(nameof(auditModel), auditModel, null)
    };
  }
}
