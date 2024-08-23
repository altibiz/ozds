using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum TopicModel
{
  General
}

public static class TopicModelExtensions
{
  public static TopicModel ToModel(this TopicEntity entity)
  {
    return entity switch
    {
      TopicEntity.General => TopicModel.General,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }

  public static TopicEntity ToEntity(this TopicModel model)
  {
    return model switch
    {
      TopicModel.General => TopicEntity.General,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static string ToTitle(this TopicModel model)
  {
    return model switch
    {
      TopicModel.General => "General",
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
