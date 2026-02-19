using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class ActionModelCachingEntityConverter
  : ConcreteModelCachingEntityConverter<ActionModel, ActionEntity>
{
  public override ActionEntity ToEntity(ActionModel model)
  {
    return model switch
    {
      ActionModel.Read => ActionEntity.Read,
      ActionModel.List => ActionEntity.List,
      ActionModel.Create => ActionEntity.Create,
      ActionModel.Update => ActionEntity.Update,
      ActionModel.Delete => ActionEntity.Delete,
      ActionModel.Restore => ActionEntity.Restore,
      ActionModel.Forget => ActionEntity.Forget,
      _ => throw new NotImplementedException(),
    };
  }

  public override ActionModel ToModel(ActionEntity entity)
  {
    return entity switch
    {
      ActionEntity.Read => ActionModel.Read,
      ActionEntity.List => ActionModel.List,
      ActionEntity.Create => ActionModel.Create,
      ActionEntity.Update => ActionModel.Update,
      ActionEntity.Delete => ActionModel.Delete,
      ActionEntity.Restore => ActionModel.Restore,
      ActionEntity.Forget => ActionModel.Forget,
      _ => throw new NotImplementedException(),
    };
  }
}
