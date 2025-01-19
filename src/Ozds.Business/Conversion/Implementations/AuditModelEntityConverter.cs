using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations;

public class AuditModelEntityConverter
  : ConcreteModelEntityConverter<AuditModel, AuditEntity>
{
  public override AuditEntity ToEntity(AuditModel model)
  {
    return model switch
    {
      AuditModel.Query => AuditEntity.Query,
      AuditModel.Creation => AuditEntity.Creation,
      AuditModel.Modification => AuditEntity.Modification,
      AuditModel.Deletion => AuditEntity.Deletion,
      AuditModel.Restoration => AuditEntity.Restoration,
      AuditModel.Forgetting => AuditEntity.Forgetting,
      _ => throw new NotImplementedException()
    };
  }

  public override AuditModel ToModel(AuditEntity entity)
  {
    return entity switch
    {
      AuditEntity.Query => AuditModel.Query,
      AuditEntity.Creation => AuditModel.Creation,
      AuditEntity.Modification => AuditModel.Modification,
      AuditEntity.Deletion => AuditModel.Deletion,
      AuditEntity.Restoration => AuditModel.Restoration,
      AuditEntity.Forgetting => AuditModel.Forgetting,
      _ => throw new NotImplementedException()
    };
  }
}
