using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Users.Entities;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class PasswordModelEntityConverter
  : ConcreteModelUserEntityConverter<PasswordModel, PasswordEntity>
{
  public override void InitializeEntity(
    PasswordModel model,
    PasswordEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.UserId = model.UserId;
    entity.OldPassword = model.OldPassword;
    entity.NewPassword = model.NewPassword;
  }

  public override void InitializeModel(
    PasswordEntity entity,
    PasswordModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UserId = entity.UserId;
    model.OldPassword = entity.OldPassword;
    model.NewPassword = entity.NewPassword;
  }
}
