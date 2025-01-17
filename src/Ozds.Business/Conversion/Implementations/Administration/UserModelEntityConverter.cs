using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Users.Entities;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class
  UserModelEntityConverter : ConcreteModelEntityConverter<UserModel, UserEntity>
{
  protected override UserEntity ToEntity(UserModel model)
  {
    return model.ToEntity();
  }

  protected override UserModel ToModel(UserEntity entity)
  {
    return entity.ToModel();
  }
}

public static class UserModelEntityConverterExtensions
{
  public static UserEntity ToEntity(this UserModel model)
  {
    return new UserEntity(
      model.Id,
      model.UserName,
      model.Email
    );
  }

  public static UserModel ToModel(this UserEntity entity)
  {
    return new UserModel
    {
      Id = entity.Id,
      UserName = entity.UserName,
      Email = entity.Email
    };
  }
}
