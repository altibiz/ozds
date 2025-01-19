using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class RoleModelEntityConverter
  : ConcreteModelEntityConverter<RoleModel, RoleEntity>
{
  public override RoleEntity ToEntity(RoleModel model)
  {
    return model switch
    {
      RoleModel.OperatorRepresentative => RoleEntity.OperatorRepresentative,
      RoleModel.LocationRepresentative => RoleEntity.LocationRepresentative,
      RoleModel.NetworkUserRepresentative => RoleEntity.NetworkUserRepresentative,
      _ => throw new NotImplementedException()
    };
  }

  public override RoleModel ToModel(RoleEntity entity)
  {
    return entity switch
    {
      RoleEntity.OperatorRepresentative => RoleModel.OperatorRepresentative,
      RoleEntity.LocationRepresentative => RoleModel.LocationRepresentative,
      RoleEntity.NetworkUserRepresentative => RoleModel.NetworkUserRepresentative,
      _ => throw new NotImplementedException()
    };
  }
}
