using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Users.Entities;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class UserWithPasswordModelEntityConverter(
  IServiceProvider serviceProvider)
  : ConcreteModelEntityConverter<UserWithPasswordModel, UserWithPasswordEntity>
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    UserWithPasswordModel model,
    UserWithPasswordEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.User = modelEntityConverter.ToEntity<UserEntity>(model.User);
    entity.OldPassword = model.OldPassword;
    entity.NewPassword = model.NewPassword;
  }

  public override void InitializeModel(
    UserWithPasswordEntity entity,
    UserWithPasswordModel model
  )
  {
    base.InitializeModel(entity, model);
    model.User = modelEntityConverter.ToModel<UserModel>(entity.User);
    model.OldPassword = entity.OldPassword;
    model.NewPassword = entity.NewPassword;
  }
}
