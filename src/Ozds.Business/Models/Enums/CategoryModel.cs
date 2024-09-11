using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum CategoryModel
{
  All,
  Messenger,
  MessengerPush,
  Audit
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
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
