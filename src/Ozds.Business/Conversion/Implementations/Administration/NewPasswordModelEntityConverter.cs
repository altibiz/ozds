using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Users.Entities;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class NewPasswordModelEntityConverter
  : ConcreteModelUserEntityConverter<NewPasswordModel, NewPasswordEntity>
{
  public override void InitializeEntity(
    NewPasswordModel model,
    NewPasswordEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UserId = model.UserId;
    entity.NewPassword = model.NewPassword;
  }

  public override void InitializeModel(
    NewPasswordEntity entity,
    NewPasswordModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UserId = entity.UserId;
    model.NewPassword = entity.NewPassword;
  }
}
