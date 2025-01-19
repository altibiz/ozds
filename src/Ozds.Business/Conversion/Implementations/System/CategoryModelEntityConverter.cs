using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class CategoryModelEntityConverter
  : ConcreteModelEntityConverter<CategoryModel, CategoryEntity>
{
  public override CategoryEntity ToEntity(CategoryModel model)
  {
    return model switch
    {
      CategoryModel.All => CategoryEntity.All,
      CategoryModel.Messenger => CategoryEntity.Messenger,
      CategoryModel.MessengerPush => CategoryEntity.MessengerPush,
      CategoryModel.Audit => CategoryEntity.Audit,
      CategoryModel.Error => CategoryEntity.Error,
      CategoryModel.Lifecycle => CategoryEntity.Lifecycle,
      _ => throw new NotImplementedException()
    };
  }

  public override CategoryModel ToModel(CategoryEntity entity)
  {
    return entity switch
    {
      CategoryEntity.All => CategoryModel.All,
      CategoryEntity.Messenger => CategoryModel.Messenger,
      CategoryEntity.MessengerPush => CategoryModel.MessengerPush,
      CategoryEntity.Audit => CategoryModel.Audit,
      CategoryEntity.Error => CategoryModel.Error,
      CategoryEntity.Lifecycle => CategoryModel.Lifecycle,
      _ => throw new NotImplementedException()
    };
  }
}
