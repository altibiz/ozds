using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum CategoryModel
{
  All,
  Messenger,
  MessengerPush,
  Audit,
  Error,
  Lifecycle
}

public static class CategoryModelExtensions
{
  public static CategoryModel ToModel(this CategoryEntity entity)
  {
    return entity switch
    {
      CategoryEntity.All => CategoryModel.All,
      CategoryEntity.Messenger => CategoryModel.Messenger,
      CategoryEntity.MessengerPush => CategoryModel.MessengerPush,
      CategoryEntity.Audit => CategoryModel.Audit,
      CategoryEntity.Error => CategoryModel.Error,
      CategoryEntity.Lifecycle => CategoryModel.Lifecycle,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }

  public static CategoryEntity ToEntity(this CategoryModel model)
  {
    return model switch
    {
      CategoryModel.All => CategoryEntity.All,
      CategoryModel.Messenger => CategoryEntity.Messenger,
      CategoryModel.MessengerPush => CategoryEntity.MessengerPush,
      CategoryModel.Audit => CategoryEntity.Audit,
      CategoryModel.Error => CategoryEntity.Error,
      CategoryModel.Lifecycle => CategoryEntity.Lifecycle,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static string ToTitle(this CategoryModel model)
  {
    return model switch
    {
      CategoryModel.All => "General",
      CategoryModel.Messenger => "Messenger",
      CategoryModel.MessengerPush => "Messenger push",
      CategoryModel.Audit => "Audit",
      CategoryModel.Error => "Error",
      CategoryModel.Lifecycle => "Lifecycle",
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
