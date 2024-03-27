using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum RoleModel
{
  OperatorRepresentative,

  LocationRepresentative,

  NetworkUserRepresentative
}

public static class RoleModelExtensions
{
  public static RoleModel ToModel(this RoleEntity entity) =>
    entity switch
    {
      RoleEntity.OperatorRepresentative => RoleModel.OperatorRepresentative,
      RoleEntity.LocationRepresentative => RoleModel.LocationRepresentative,
      RoleEntity.NetworkUserRepresentative => RoleModel.NetworkUserRepresentative,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };

  public static RoleEntity ToEntity(this RoleModel model) =>
    model switch
    {
      RoleModel.OperatorRepresentative => RoleEntity.OperatorRepresentative,
      RoleModel.LocationRepresentative => RoleEntity.LocationRepresentative,
      RoleModel.NetworkUserRepresentative => RoleEntity.NetworkUserRepresentative,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
}
