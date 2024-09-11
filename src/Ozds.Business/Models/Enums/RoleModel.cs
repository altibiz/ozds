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
  public static RoleModel ToModel(this RoleEntity entity)
  {
    return entity switch
    {
      RoleEntity.OperatorRepresentative => RoleModel.OperatorRepresentative,
      RoleEntity.LocationRepresentative => RoleModel.LocationRepresentative,
      RoleEntity.NetworkUserRepresentative => RoleModel
        .NetworkUserRepresentative,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }

  public static RoleEntity ToEntity(this RoleModel model)
  {
    return model switch
    {
      RoleModel.OperatorRepresentative => RoleEntity.OperatorRepresentative,
      RoleModel.LocationRepresentative => RoleEntity.LocationRepresentative,
      RoleModel.NetworkUserRepresentative => RoleEntity
        .NetworkUserRepresentative,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static List<TopicModel> ToTopics(this RoleModel model)
  {
    return model switch
    {
      RoleModel.OperatorRepresentative => new List<TopicModel>
      {
        TopicModel.All
      },
      RoleModel.LocationRepresentative => new List<TopicModel>(),
      RoleModel.NetworkUserRepresentative => new List<TopicModel>(),
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
