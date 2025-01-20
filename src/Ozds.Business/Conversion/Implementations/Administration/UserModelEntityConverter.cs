using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Users.Entities;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class UserModelEntityConverter
  : ConcreteModelEntityConverter<UserModel, UserEntity>
{
  public override void InitializeEntity(
    UserModel model,
    UserEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UserName = model.UserName;
    entity.Email = model.Email;
    entity.Id = model.Id;
  }

  public override void InitializeModel(
    UserEntity entity,
    UserModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UserName = entity.UserName;
    model.Email = entity.Email;
    model.Id = entity.Id;
  }
}
