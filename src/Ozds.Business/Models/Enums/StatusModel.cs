using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum StatusModel
{
  Unseen,
  Seen,
  Resolved,
}

public static class StatusModelExtensions
{
  public static StatusModel ToModel(this StatusEntity entity)
  {
    return entity switch
    {
      StatusEntity.Unseen => StatusModel.Unseen,
      StatusEntity.Seen => StatusModel.Seen,
      StatusEntity.Resolved => StatusModel.Resolved,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }

  public static StatusEntity ToEntity(this StatusModel model)
  {
    return model switch
    {
      StatusModel.Unseen => StatusEntity.Unseen,
      StatusModel.Seen => StatusEntity.Seen,
      StatusModel.Resolved => StatusEntity.Resolved,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
